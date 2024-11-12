using CaseClient.Core;

namespace CaseClient.Application.ViewModels;

public record Response(Object Data, bool Sucess, int? limit, int?  offset);