using TradeTOS.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TradeTOS.Utilities
{
    public class Globalization
    {
        public static readonly BindableProperty KeyProperty =
            BindableProperty.CreateAttached(
                "Key",
                typeof(string),
                typeof(Globalization),
                string.Empty,
                propertyChanged: OnKeyChanged);

        public static string GetKey(BindableObject view)
        {
            return (string)view.GetValue(KeyProperty);
        }

        public static void SetKey(BindableObject view, string value)
        {
            view.SetValue(KeyProperty, value);
        }

        static void OnKeyChanged(BindableObject view, object oldValue, object newValue)
        {
            if (view.GetType() == typeof(Label))
            {
                SubsribeForLabelText(GetKey(view), view as Label);
            }
            else if (view.GetType() == typeof(Entry))
            {
                SubsribeForEntryPlaceHolder(GetKey(view), view as Entry);
            }
            else if (view.GetType() == typeof(ContentPage))
            {
                SubsribeForPageTitle(GetKey(view), view as Page);
            }
            else if (view.GetType() == typeof(Button))
            {
                SubsribeForButtonText(GetKey(view), view as Button);
            }
            else if (view.GetType() == typeof(ContentView))
            {
                SubsribeForParentPageTitle(GetKey(view), view as ContentView);
            }
            else if (view.GetType() == typeof(BindablePicker))
            {
                SubsribeForGBGBindablePickerTitle(GetKey(view), view as BindablePicker);
            }
            else
            {
                SubsribeForPageTitle(GetKey(view), view as ContentPage);
            }
        }

        public static readonly BindableProperty ShowTitleProperty =
            BindableProperty.CreateAttached(
                "ShowTitle",
                typeof(bool),
                typeof(Globalization),
                false,
                propertyChanged: OnShowTitleChanged);

        public static bool GetShowTitle(BindableObject view)
        {
            return (bool)view.GetValue(ShowTitleProperty);
        }

        public static void SetShowTitle(BindableObject view, bool value)
        {
            view.SetValue(ShowTitleProperty, value);
        }

        static void OnShowTitleChanged(BindableObject view, object oldValue, object newValue)
        {
            if (GetShowTitle(view))
            {
                SubsribeForPageTitle(view as Page);
            }
        }

        private static void SubsribeForLabelText(string key, Label labelElement)
        {
            if (!string.IsNullOrEmpty(key))
            {
                if (labelElement == null)
                {
                    return;
                }
                labelElement.Text = ResourceManager.Instance.GetLabel(key);
                MessagingCenter.Subscribe<ResourceManager>(labelElement, "CultureChanged", (sender) =>
                {
                    labelElement.Text = ResourceManager.Instance.GetLabel(key);
                });

            }
        }

        private static void SubsribeForEntryPlaceHolder(string key, Entry entryElement)
        {
            if (!string.IsNullOrEmpty(key))
            {
                if (entryElement == null)
                {
                    return;
                }
                entryElement.Placeholder = ResourceManager.Instance.GetPlaceHolder(key);
                MessagingCenter.Subscribe<ResourceManager>(entryElement, "CultureChanged", (sender) =>
                {
                    entryElement.Placeholder = ResourceManager.Instance.GetPlaceHolder(key);
                });

            }
        }

        private static void SubsribeForGBGBindablePickerTitle(string key, BindablePicker pickerElement)
        {
            if (!string.IsNullOrEmpty(key))
            {
                if (pickerElement == null)
                {
                    return;
                }
                pickerElement.Title = ResourceManager.Instance.GetPlaceHolder(key);
                MessagingCenter.Subscribe<ResourceManager>(pickerElement, "CultureChanged", (sender) =>
                {
                    pickerElement.Title = ResourceManager.Instance.GetPlaceHolder(key);
                });

            }
        }

        private static void SubsribeForPageTitle(string key, Page pageElement)
        {
            if (!string.IsNullOrEmpty(key))
            {
                if (pageElement == null)
                {
                    return;
                }
                pageElement.BackgroundColor = Color.White;
                pageElement.Title = ResourceManager.Instance.GetTitle(key);
                MessagingCenter.Subscribe<ResourceManager>(pageElement, "CultureChanged", (sender) =>
                {
                    var title = ResourceManager.Instance.GetTitle(key);
                    if (!string.IsNullOrEmpty(title))
                    {
                        pageElement.Title = title;
                    }
                });

            }
        }

        private static void SubsribeForPageTitle(Page pageElement)
        {
            if (pageElement == null)
            {
                return;
            }
            pageElement.Title = ResourceManager.Instance.GetTitle();
            MessagingCenter.Subscribe<ResourceManager>(pageElement, "CultureChanged", (sender) =>
            {
                pageElement.Title = ResourceManager.Instance.GetTitle();
            });
        }

        private static void SubsribeForParentPageTitle(string key, ContentView viewElement)
        {
            if (!string.IsNullOrEmpty(key))
            {
                Page pageElement = viewElement.Parent as Page;
                if (pageElement == null)
                {
                    return;
                }
                pageElement.BackgroundColor = Color.White;
                pageElement.Title = ResourceManager.Instance.GetTitle(key);
                MessagingCenter.Subscribe<ResourceManager>(pageElement, "CultureChanged", (sender) =>
                {
                    var title = ResourceManager.Instance.GetTitle(key);
                    if (!string.IsNullOrEmpty(title))
                    {
                        pageElement.Title = title;
                    }
                });

            }
        }

        private static void SubsribeForPageTitle(string key, ContentPage pageElement)
        {
            if (!string.IsNullOrEmpty(key))
            {
                if (pageElement == null)
                {
                    return;
                }
                pageElement.BackgroundColor = Color.White;
                pageElement.Title = ResourceManager.Instance.GetTitle(key);
                MessagingCenter.Subscribe<ResourceManager>(pageElement, "CultureChanged", (sender) =>
                {
                    var title = ResourceManager.Instance.GetTitle(key);
                    if (!string.IsNullOrEmpty(title))
                    {
                        pageElement.Title = title;
                    }
                });

            }
        }

        private static void SubsribeForButtonText(string key, Button buttonElement)
        {
            if (!string.IsNullOrEmpty(key))
            {
                if (buttonElement == null)
                {
                    return;
                }
                buttonElement.Text = ResourceManager.Instance.GetButtonText(key);
                MessagingCenter.Subscribe<ResourceManager>(buttonElement, "CultureChanged", (sender) =>
                {
                    buttonElement.Text = ResourceManager.Instance.GetButtonText(key);
                });

            }
        }

    }
}
