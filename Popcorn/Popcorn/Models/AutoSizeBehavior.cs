using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Popcorn.Models
{
    public class AutoSizeBehavior : Behavior<ListView>
    {
        ListView _ListView;

        protected override void OnAttachedTo(ListView bindable)
        {
            _ListView = bindable;
            bindable.HeightRequest = 0;
            bindable.ItemAppearing += AppearanceChanged;
            bindable.ItemDisappearing += AppearanceChanged;
        }

        protected override void OnDetachingFrom(ListView bindable)
        {
            bindable.ItemAppearing -= AppearanceChanged;
            bindable.ItemDisappearing -= AppearanceChanged;
            _ListView = null;
        }

        private void AppearanceChanged(object sender, ItemVisibilityEventArgs e) =>
          UpdateHeight(e.Item);

        void UpdateHeight(object item)
        {
            double height;
            if (_ListView.HasUnevenRows)
            {
                if ((height = _ListView.HeightRequest) == (double)VisualElement.HeightRequestProperty.DefaultValue)
                    height = 0;

                height += MeasureRowHeight(item);
            }
            else
            {
                height = _ListView.RowHeight;
                if (height == (int)ListView.RowHeightProperty.DefaultValue)
                    _ListView.RowHeight = (int)(height = MeasureRowHeight(item));
            }
            SetHeight(height);
        }

        double MeasureRowHeight(object item)
        {

            var template = _ListView.ItemTemplate;
            var cell = (Cell)template.CreateContent();

            cell.BindingContext = item;

            // TODO: redundant
            cell.ForceUpdateSize();

            var height = cell.RenderHeight;
            var mod = height % 1;
            if (mod > 0)
                height = height - mod + 1;
            return height;
        }

        void SetHeight(double rowHeight)
        {
            var height = 0d;
            // TODO if header or footer is string etc.
            if (_ListView.Header is VisualElement header)
                height += header.Height;
            if (_ListView.Footer is VisualElement footer)
                height += footer.Height;

            var tiv = (ITemplatedItemsView<Cell>)_ListView;
            height += tiv.TemplatedItems.Count * rowHeight;

            _ListView.HeightRequest = height;
        }
    }
}
