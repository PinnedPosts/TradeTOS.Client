using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TradeTOS.Controls
{
  
    public class BindablePicker : Picker
    {
        public static readonly BindableProperty ItemsSourceProperty =
                   BindableProperty.CreateAttached(
                       "ItemsSource",
                       typeof(IEnumerable),
                       typeof(BindablePicker),
                       default(IEnumerable),
                       propertyChanged: onItemsSourceChanged);


        public static readonly BindableProperty SelectedItemProperty =
                   BindableProperty.CreateAttached(
                       "SelectedItem",
                       typeof(object),
                       typeof(BindablePicker),
                       default(object),
                       BindingMode.TwoWay,
                       propertyChanged: onSelectedItemChanged);

        public static readonly BindableProperty SelectedValueProperty =
                   BindableProperty.CreateAttached(
                       "SelectedValue",
                       typeof(object),
                       typeof(BindablePicker),
                       null,
                       propertyChanged: null);

        public static readonly BindableProperty SelectedValuePathProperty =
                   BindableProperty.CreateAttached(
                       "SelectedValuePath",
                       typeof(object),
                       typeof(BindablePicker),
                       null,
                       propertyChanged: null);

        public static readonly BindableProperty DisplayMemberProperty =
         BindableProperty.CreateAttached(
             "DisplayMember",
             typeof(string),
             typeof(BindablePicker),
             null,
             propertyChanged: null);


        public BindablePicker()
        {
            SelectedIndexChanged += onSelectedIndexChanged;
        }

        public List<object> ItemsAsObject = new List<object>();

        private static void onItemsSourceChanged(BindableObject bindableObject, object oldvalueObject, object newvalueObject)
        {
            IEnumerable oldvalue = oldvalueObject as IEnumerable;
            IEnumerable newvalue = newvalueObject as IEnumerable;
            var bindableObjectPicker = bindableObject as BindablePicker;
            if (bindableObjectPicker == null) return;
            bindableObjectPicker.Items.Clear();              //This is the List with the string representation of the the Objects from the bound List
            bindableObjectPicker.ItemsAsObject.Clear();      //This is the bound List with the Objects
            if (newvalue != null)
            {
                foreach (var item in newvalue)
                {
                    if(item.GetType() == typeof(string))
                    {
                        string itemValue = (string)item;
                        bindableObjectPicker.Items.Add((itemValue ?? "").ToString());
                        bindableObjectPicker.ItemsAsObject.Add(item);
                    }
                    else
                    {
                        PropertyInfo pi = item.GetType().GetRuntimeProperty(bindableObjectPicker.DisplayMember);
                        if (pi != null)
                        {
                            string itemValue = (string)pi.GetValue(item);
                            bindableObjectPicker.Items.Add((itemValue ?? "").ToString());
                            bindableObjectPicker.ItemsAsObject.Add(item);
                        }
                    }
                }
            }
        }

        private static void onSelectedItemChanged(BindableObject bindableObject, object oldValue, object newValue)
        {
            var bindableObjectPicker = bindableObject as BindablePicker;
            if (newValue != null && bindableObjectPicker != null)
            {
                if (newValue.GetType() == typeof(string))
                {
                    string itemValue = (string)newValue;
                    if (!string.IsNullOrEmpty(itemValue))
                    {
                        bindableObjectPicker.SelectedIndex = bindableObjectPicker.Items.IndexOf(itemValue);
                    }
                    else
                    {
                        bindableObjectPicker.SelectedIndex = 0;
                    }
                }
                else
                {
                    PropertyInfo pi = newValue.GetType().GetRuntimeProperty(bindableObjectPicker.DisplayMember);
                    if (pi != null)
                    {
                        string itemValue = (string)pi.GetValue(newValue);
                        bindableObjectPicker.SelectedIndex = bindableObjectPicker.Items.IndexOf(itemValue);
                    }
                }
            }
        }

        private void onSelectedIndexChanged(object sender, EventArgs ev)
        {
            if (Items == null || Items.Count == 0)
            {
                return;
            }
            if (SelectedIndex < 0 || SelectedIndex > Items.Count - 1)
            {
                SelectedItem = null;
            }
            else
            {
                var bindableObjectPicker = sender as BindablePicker;
                SelectedItem = bindableObjectPicker?.ItemsAsObject[SelectedIndex];
                if (!string.IsNullOrEmpty(SelectedValuePath))
                    SelectedValue = SelectedItem?.GetType().GetRuntimeProperty(SelectedValuePath).GetValue(SelectedItem);
            }
        }

        public IEnumerable ItemsSource
        {
            get { return (IEnumerable)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }


        public string SelectedValuePath
        {
            get { return (string)GetValue(SelectedValuePathProperty); }
            set { SetValue(SelectedValuePathProperty, value); }
        }

        public object SelectedValue
        {
            get { return GetValue(SelectedValueProperty); }
            set { SetValue(SelectedValueProperty, value); }
        }

        public object SelectedItem
        {
            get { return GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }

        public string DisplayMember
        {
            get { return (string)GetValue(DisplayMemberProperty); }
            set { SetValue(DisplayMemberProperty, value); }
        }
    }
}
