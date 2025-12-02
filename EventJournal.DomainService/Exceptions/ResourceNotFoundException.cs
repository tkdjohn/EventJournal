using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace EventJournal.DomainService.Exceptions {
    //TODO: add logging?
    //TODO: consider making this a generic NotFoundException that takes the type that was not found?
    //TODO: [Serializable] ?
    //TODO: is this needed?
    public class ResourceNotFoundException : Exception {
        public ResourceNotFoundException() { }

        public ResourceNotFoundException(string message) : base(message) {
        }

        public ResourceNotFoundException(string? message, Exception? innerException) : base(message, innerException) {
        }

        public static void ThrowIfNull([NotNull] object? argument, [CallerArgumentExpression(nameof(argument))] string? paramName = null) {
            if (argument is null) {
                Throw(paramName);
            }
        }

        [DoesNotReturn]
        internal static void Throw(string? paramName) => throw new ArgumentNullException(paramName);
    }
}
