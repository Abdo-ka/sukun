import 'package:core/core.dart';
import 'package:equatable/equatable.dart';
import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:injectable/injectable.dart';
import 'package:mobile/features/others/domain/entities/other_entity.dart';
import 'package:mobile/features/others/domain/repositories/others_repository.dart';

part 'others_state.dart';

// Fixed: Switched to Cubit as requested for simpler state management
@injectable
class OthersCubit extends Cubit<OthersState> {
  final OthersRepository _repository;

  OthersCubit(this._repository) : super(const OthersState());

  Future<void> getCategories() async {
    emit(state.copyWith(status: const BlocStatus.loading()));
    final result = await _repository.getCategories();
    result.fold(
      (left) =>
          emit(state.copyWith(status: BlocStatus.failure(left))),
      (right) => emit(state.copyWith(
          status: const BlocStatus.success(), categories: right)),
    );
  }

  Future<void> getItems(int categoryId) async {
    emit(state.copyWith(status: const BlocStatus.loading()));
    final result = await _repository.getItems(categoryId: categoryId);
    result.fold(
      (left) =>
          emit(state.copyWith(status: BlocStatus.failure(left))),
      (right) =>
          emit(state.copyWith(status: const BlocStatus.success(), items: right)),
    );
  }
}
