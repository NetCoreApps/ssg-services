﻿using ServiceStack;
using ServiceStack.DataAnnotations;
using SsgServices.ServiceModel.Types;

namespace SsgServices.ServiceModel;

[Renderer(typeof(RenderSimpleText))]
[Tag(Tag.Mail), ValidateIsAdmin]
[Description("Simple Text Email")]
public class SimpleTextEmail : CreateEmailBase, IPost, IReturn<MailMessage>
{
    [ValidateNotEmpty]
    [FieldCss(Field = "col-span-12")]
    public string Subject { get; set; }

    [ValidateNotEmpty]
    [Input(Type = "textarea"), FieldCss(Field = "col-span-12", Input = "h-36")]
    public string Body { get; set; }
    public bool? Draft { get; set; }
}

[Renderer(typeof(RenderCustomHtml), Layout = "layout", Page="markdown")]
[Tag(Tag.Mail), ValidateIsAdmin]
[Icon(Svg = Icons.TextMarkup)]
[Description("Markdown Email")]
public class MarkdownEmail : CreateEmailBase, IPost, IReturn<MailMessage>
{
    [ValidateNotEmpty]
    [FieldCss(Field = "col-span-12")]
    public string Subject { get; set; }

    [ValidateNotEmpty]
    [Input(Type = "MarkdownInput", Label = ""), FieldCss(Field = "col-span-12", Input = "h-56")]
    public string? Body { get; set; }
    public bool? Draft { get; set; }
}

[Renderer(typeof(RenderCustomHtml))]
[Tag(Tag.Mail), ValidateIsAdmin]
[Icon(Svg = Icons.RichHtml)]
[Description("Custom HTML Email")]
public class CustomHtmlEmail : CreateEmailBase, IPost, IReturn<MailMessage>
{
    [ValidateNotEmpty]
    [Input(Type = "combobox", EvalAllowableValues = "AppData.EmailLayoutOptions")]
    public string Layout { get; set; }
    
    [ValidateNotEmpty]
    [Input(Type = "combobox", EvalAllowableValues = "AppData.EmailPageOptions")]
    public string Page { get; set; }
    
    [ValidateNotEmpty]
    [FieldCss(Field = "col-span-12")]
    public string Subject { get; set; }

    [Input(Type = "MarkdownInput", Label = ""), FieldCss(Field = "col-span-12", Input = "h-56")]
    public string? Body { get; set; }
    public bool? Draft { get; set; }
}
