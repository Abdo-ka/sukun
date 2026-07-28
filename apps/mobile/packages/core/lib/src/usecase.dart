import 'type_definitions.dart';

/// Base use case interface following Clean Architecture principles
/// Implements single responsibility with typed parameters and results
///
/// Example:
/// ```dart
/// class GetUserUseCase implements UseCase<User, GetUserParams> {
///   @override
///   FutureResult<User> call(GetUserParams params) async {
///     // Implementation
///   }
/// }
/// ```
abstract class UseCase<T, Params> {
  FutureResult<T> call(Params params);
}

/// Use this when your use case doesn't require parameters
class NoParams {
  const NoParams();
}
