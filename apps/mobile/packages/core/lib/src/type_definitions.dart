import 'package:core/src/error/app_exception.dart';
import 'package:either_dart/either.dart';

/// Type alias for asynchronous operations that return either an error or success value
/// Uses Either monad for functional error handling
/// - Left: AppException (error case)
/// - Right: T (success case)
typedef FutureResult<T> = Future<Either<AppException, T>>;
