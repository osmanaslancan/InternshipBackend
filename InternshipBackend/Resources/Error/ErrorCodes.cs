﻿namespace InternshipBackend.Resources.Error;

public static class ErrorCodes
{
    public const string UserNotFound = nameof(UserNotFound);
    public const string EmailNotFound = nameof(EmailNotFound);
    public const string EmailExists = nameof(EmailExists);
    public const string UserAlreadyExists = nameof(UserAlreadyExists);
    public const string InsufficientPermission = nameof(InsufficientPermission);
    public const string InvalidPhoto = nameof(InvalidPhoto);
    public const string CompanyNotFound = nameof(CompanyNotFound);
    public const string ExceededComments = nameof(ExceededComments);
    public const string CommentOpenPosting = nameof(CommentOpenPosting);
}
