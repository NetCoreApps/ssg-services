﻿using System;
using System.Collections.Generic;
using ServiceStack;
using ServiceStack.DataAnnotations;

namespace SsgServices.ServiceModel.Types;

[Icon(Svg = Icons.Contact)]
public class Contact
{
    [AutoIncrement]
    public int Id { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public Source Source { get; set; }
    [FormatEnumFlags(nameof(MailingList))]
    public MailingList MailingLists { get; set; }
    public string Token { get; set; }
    [Index(Unique = true)]
    public string EmailLower { get; set; }
    [Index]
    public string NameLower { get; set; }
    [Index(Unique = true)]
    public string ExternalRef { get; set; }
    public int? AppUserId { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? VerifiedDate { get; set; }
    public DateTime? DeletedDate { get; set; }
    public DateTime? UnsubscribedDate { get; set; }
}

[Icon(Svg = Icons.Mail)]
public class MailMessage
{
    [AutoIncrement]
    public int Id { get; set; }
    public string ExternalRef { get; set; }
    public string Email { get; set; }
    public string Layout { get; set; }
    public string Page { get; set; }
    public string Renderer { get; set; }
    public Dictionary<string,object> RendererArgs { get; set; }
    public EmailMessage Message { get; set; }
    public bool Draft { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? StartedDate { get; set; }
    public DateTime? CompletedDate { get; set; }
    public string? ErrorCode { get; set; }
    public string? ErrorMessage { get; set; }
}

[Icon(Svg = Icons.MailRun)]
public class MailRun
{
    [AutoIncrement]
    public int Id { get; set; }
    [FormatEnumFlags(nameof(MailingList))]
    public MailingList MailingList { get; set; }
    public string Generator { get; set; }
    public Dictionary<string,object> GeneratorArgs { get; set; }
    public string Layout { get; set; }
    public string Page { get; set; }
    public string ExternalRef { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? GeneratedDate { get; set; }
    public DateTime? SentDate { get; set; }
    public DateTime? CompletedDate { get; set; }
    public int EmailsCount { get; set; }
}

[Icon(Svg = Icons.Mail)]
[UniqueConstraint(nameof(MailRunId), nameof(ContactId))]
public class MailMessageRun
{
    [AutoIncrement]
    public int Id { get; set; }
    [ForeignKey(typeof(MailRun), OnDelete = "CASCADE")]
    public int MailRunId { get; set; }
    [Ref(Model = nameof(Contact), RefId = "Id", RefLabel = "Email")]
    public int ContactId { get; set; }
    [Reference]
    [Format(FormatMethods.Hidden)]
    public Contact Contact { get; set; }
    public string Renderer { get; set; }
    public Dictionary<string,object> RendererArgs { get; set; }
    public string ExternalRef { get; set; }
    public EmailMessage Message { get; set; }
    public DateTime? StartedDate { get; set; }
    public DateTime? CompletedDate { get; set; }
    public string? ErrorCode { get; set; }
    public string? ErrorMessage { get; set; }
}

public enum Source
{
    Unknown,
    UI,
    Website,
}

[Flags]
public enum MailingList
{
    None = 0,
    [Description("Test Group")]
    TestGroup = 1 << 0,
    [Description("Monthly Newsletter")]
    MonthlyNewsletter = 1 << 1,
    [Description("New Blog Posts")]
    BlogPostReleases = 1 << 2,
    [Description("New Videos")]
    VideoReleases = 1 << 3,
    [Description("New Product Releases")]
    ProductReleases = 1 << 4,
    [Description("Yearly Updates")]
    YearlyUpdates = 1 << 5,
}

public class MailTo
{
    public string Email { get; set; }
    public string Name { get; set; }
}
public class EmailMessage
{
    public List<MailTo> To { get; set; }
    public List<MailTo> Cc { get; set; }
    public List<MailTo> Bcc { get; set; }
    public MailTo? From { get; set; }
    public string Subject { get; set; }
    public string? BodyHtml { get; set; }
    public string? BodyText { get; set; }
}

public class Archive
{
    public string Name { get; set; }
    public int ContactCount { get; set; }
    public int MailMessageCount { get; set; }
    public int MailRunCount { get; set; }
    public int MailMessageRun { get; set; }
    public DateTime LastUpdated { get; set; }
}

