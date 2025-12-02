// 📦 Package imports:
import 'package:dio/dio.dart';

// 🌎 Project imports:
import 'app_exception.dart';

export 'app_exception.dart';
export 'base_response.dart';

/// A callback that returns a Dio response, presumably from a Dio method
/// it has called which performs an HTTP request, such as `dio.get()`,
/// `dio.post()`, etc.
typedef HttpLibraryMethod<T> = Future<Response<T>> Function();

/// Function which takes a Dio response object and optionally maps it to an
/// instance of [AppHttpClientException].
typedef ResponseExceptionMapper = AppException? Function(
    Response response, Exception e);
