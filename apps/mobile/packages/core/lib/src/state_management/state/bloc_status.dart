import 'dart:core';

import 'package:core/src/error/app_exception.dart';

enum Status { initial, loading, success, failure }

class BlocStatus {
  final Status status;
  final AppException? failure;

  const BlocStatus.loading()
      : status = Status.loading,
        failure = null;
  const BlocStatus.success()
      : status = Status.success,
        failure = null;
  const BlocStatus.failure(this.failure) : status = Status.failure;
  const BlocStatus.initial()
      : status = Status.initial,
        failure = null;

  bool isLoading() => status == Status.loading;
  bool isInitial() => status == Status.initial;
  bool isFailure() => status == Status.failure;
  bool isSuccess() => status == Status.success;
}
