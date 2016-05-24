using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace AliceApi.Helpers
{
    public static class EnumDropDownListForHelper
        {

            public static MvcHtmlString EnumDropDownListFor<TModel, TProperty>(
                    this HtmlHelper<TModel> htmlHelper,
                    Expression<Func<TModel, TProperty>> expression
                ) where TModel : class
            {
                return EnumDropDownListFor<TModel, TProperty>(
                                    htmlHelper, expression, null, null);
            }

            public static MvcHtmlString EnumDropDownListFor<TModel, TProperty>(
                    this HtmlHelper<TModel> htmlHelper,
                    Expression<Func<TModel, TProperty>> expression,
                    object htmlAttributes
                ) where TModel : class
            {
                return EnumDropDownListFor<TModel, TProperty>(
                                    htmlHelper, expression, null, htmlAttributes);
            }

            public static MvcHtmlString EnumDropDownListFor<TModel, TProperty>(
                    this HtmlHelper<TModel> htmlHelper,
                    Expression<Func<TModel, TProperty>> expression,
                    IDictionary<string, object> htmlAttributes
                ) where TModel : class
            {
                return EnumDropDownListFor<TModel, TProperty>(
                                    htmlHelper, expression, null, htmlAttributes);
            }

            public static MvcHtmlString EnumDropDownListFor<TModel, TProperty>(
                    this HtmlHelper<TModel> htmlHelper,
                    Expression<Func<TModel, TProperty>> expression,
                    string optionLabel
                ) where TModel : class
            {
                return EnumDropDownListFor<TModel, TProperty>(
                                    htmlHelper, expression, optionLabel, null);
            }

            public static MvcHtmlString EnumDropDownListFor<TModel, TProperty>(
                    this HtmlHelper<TModel> htmlHelper,
                    Expression<Func<TModel, TProperty>> expression,
                    string optionLabel,
                    IDictionary<string, object> htmlAttributes
                ) where TModel : class
            {
                string inputName = GetInputName(expression);
                return htmlHelper.DropDownList(
                                    inputName, ToSelectList(typeof(TProperty)),
                                    optionLabel, htmlAttributes);
            }

            public static MvcHtmlString EnumDropDownListFor<TModel, TProperty>(
                    this HtmlHelper<TModel> htmlHelper,
                    Expression<Func<TModel, TProperty>> expression,
                    string optionLabel,
                    object htmlAttributes
                ) where TModel : class
            {
                string inputName = GetInputName(expression);
                return htmlHelper.DropDownList(
                                    inputName, ToSelectList(typeof(TProperty)),
                                    optionLabel, htmlAttributes);
            }


            private static string GetInputName<TModel, TProperty>(
                    Expression<Func<TModel, TProperty>> expression)
            {
                if (expression.Body.NodeType == ExpressionType.Call)
                {
                    MethodCallExpression methodCallExpression
                                    = (MethodCallExpression)expression.Body;
                    string name = GetInputName(methodCallExpression);
                    return name.Substring(expression.Parameters[0].Name.Length + 1);

                }
                return expression.Body.ToString()
                            .Substring(expression.Parameters[0].Name.Length + 1);
            }

            private static string GetInputName(MethodCallExpression expression)
            {
                // p => p.Foo.Bar().Baz.ToString() => p.Foo OR throw...
                MethodCallExpression methodCallExpression
                                    = expression.Object as MethodCallExpression;
                if (methodCallExpression != null)
                {
                    return GetInputName(methodCallExpression);
                }
                return expression.Object.ToString();
            }


            private static SelectList ToSelectList(Type enumType)
            {
                List<SelectListItem> items = new List<SelectListItem>();
                foreach (var item in Enum.GetValues(enumType))
                {
                    FieldInfo fi = enumType.GetField(item.ToString());
                    var attribute = fi.GetCustomAttributes(
                                               typeof(DescriptionAttribute), true)
                                          .FirstOrDefault();
                    var title = attribute == null ? item.ToString()
                                      : ((DescriptionAttribute)attribute).Description;
                    var listItem = new SelectListItem
                    {
                        Value = item.ToString(),
                        Text = title,
                    };
                    items.Add(listItem);
                }

                return new SelectList(items, "Value", "Text");
            }
        }
        //    public static MvcHtmlString DropDownListFor<TModel, TElem>(
        //    this HtmlHelper<TModel> htmlHelper,
        //    Expression<Func<TModel, string>> expression,
        //    IEnumerable<TElem> elements,
        //    Func<TElem, string> valueFunc,
        //    Func<TElem, string> textFunc
        //)
        //    {
        //        ModelMetadata metadata =
        //            ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);

        //        IEnumerable<SelectListItem> items =
        //            elements.Select(elem => new SelectListItem()
        //            {
        //                Value = valueFunc(elem),
        //                Text = textFunc(elem),
        //                Selected = elem.Equals(metadata.Model)
        //            });

        //        return htmlHelper.DropDownListFor<TModel, string>(expression, items);
        //    }




        //    public abstract MvcHtmlString DropDownList(
        //        HtmlHelper htmlHelper,
        //        string name,
        //        IEnumerable<SelectListItem> selectList,
        //        string optionLabel,
        //        object htmlAttributes //     <--------------- You need to add this here
        //        );
        //}

        //public static string Label(this HtmlHelper helper, string target, string text)
        //{
        //    return $"<label for='{target}'>{text}</label>";

        //}


        //public static MvcHtmlString DropDownListForRoles<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper,
        //    Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> selectList, UserRole role,
        //    IDictionary<string, object> htmlAttributes)
        //{
        //    if (role == 'X')
        //    {
        //        //edit the select list as necessary
        //    }
        //    else if (role == 'Y')
        //    {
        //        // have the single value
        //        // add the read-only attribute to htmlAttributes
        //    }
        //    return DropDownListHelper(htmlHelper, expression, selectList, htmlAttributes);
        //}
    
}
