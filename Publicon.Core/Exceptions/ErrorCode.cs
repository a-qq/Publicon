using System;
using System.Net;

namespace Publicon.Core.Exceptions
{
    public class ErrorCode
    {
        public string Message { get; protected set; }
        public string Code { get; set; }
        public HttpStatusCode HttpStatusCode { get; }

        public ErrorCode(string message, HttpStatusCode httpStatusCode = HttpStatusCode.BadRequest)
        {
            Message = message;
            HttpStatusCode = httpStatusCode;
        }
        public static ErrorCode FieldRequired => new ErrorCode(nameof(FieldRequired));
        public static ErrorCode FileNotFound => new ErrorCode(nameof(FileNotFound), HttpStatusCode.NotFound);
        public static ErrorCode CategoryArchived => new ErrorCode(nameof(CategoryArchived));
        public static ErrorCode NotAllRequiredFieldsPresent => new ErrorCode(nameof(NotAllRequiredFieldsPresent));
        public static ErrorCode EnitityWithExistingValues(Type type) => new ErrorCode($"{type.Name}WithExistingValues");
        public static ErrorCode RequiredFieldWithExistingValues => new ErrorCode(nameof(RequiredFieldWithExistingValues));
        public static ErrorCode DefaultValueIsNull => new ErrorCode(nameof(DefaultValueIsNull));
        public static ErrorCode NameNotUnique => new ErrorCode(nameof(NameNotUnique));
        public static ErrorCode AntySpamTryAgainLater => new ErrorCode(nameof(AntySpamTryAgainLater), HttpStatusCode.Forbidden);
        public static ErrorCode EmailAlreadyVerified => new ErrorCode(nameof(EmailAlreadyVerified));
        public static ErrorCode ExpiredSecurityCode => new ErrorCode(nameof(ExpiredSecurityCode));
        public static ErrorCode InvalidSecurityCode => new ErrorCode(nameof(InvalidSecurityCode));
        public static ErrorCode InvalidUserId => new ErrorCode(nameof(InvalidUserId));
        public static ErrorCode InvalidRefreshToken => new ErrorCode(nameof(InvalidRefreshToken));
        public static ErrorCode InvalidPassword => new ErrorCode(nameof(InvalidPassword));
        public static ErrorCode UnverifiedEmail => new ErrorCode(nameof(UnverifiedEmail), HttpStatusCode.Forbidden);
        public static ErrorCode UserWithGivenEmailNotExist => new ErrorCode(nameof(UserWithGivenEmailNotExist));
        public static ErrorCode UserWithGivenEmailExist => new ErrorCode(nameof(UserWithGivenEmailExist), HttpStatusCode.UnprocessableEntity);
        public static ErrorCode InvalidCredentials => new ErrorCode(nameof(InvalidCredentials));
        public static ErrorCode DbSavingException => new ErrorCode(nameof(DbSavingException), HttpStatusCode.InternalServerError);
        public static ErrorCode EntityNotFound(Type type) => new ErrorCode($"{type.Name}NotFound", HttpStatusCode.NotFound);
        public static ErrorCode AtLeastOneOfEntitiesNotFound(Type type) => new ErrorCode($"AtLeastOneOf{type.Name}sNotFound", HttpStatusCode.NotFound);
        public static ErrorCode EntityNotExist(Type type) => new ErrorCode($"{type.Name}NotExist");
    }
}
