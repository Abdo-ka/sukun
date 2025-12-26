import 'dart:async';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:core/core.dart'; // For BlocStatus
import 'package:equatable/equatable.dart';
import 'package:injectable/injectable.dart';
import 'package:flutter/foundation.dart';
import 'package:mobile/features/others/domain/entities/other_entity.dart';
import 'package:mobile/features/others/domain/repositories/others_repository.dart';

part 'others_event.dart';
part 'others_state.dart';

@injectable
class OthersBloc extends Bloc<OthersEvent, OthersState> {
  final OthersRepository repository;

  OthersBloc(this.repository) : super(const OthersState()) {
    on<GetOthersCategoriesEvent>(_onGetCategories);
    on<GetOthersItemsEvent>(_onGetItems);
  }

  FutureOr<void> _onGetCategories(
      GetOthersCategoriesEvent event, Emitter<OthersState> emit) async {
    emit(state.copyWith(status: const BlocStatus.loading()));
    final result = await repository.getCategories();
    result.fold(
      (error) => emit(state.copyWith(
          status: BlocStatus.failure(error), errorMessage: error.message)),
      (data) => emit(
          state.copyWith(status: const BlocStatus.success(), categories: data)),
    );
  }

  FutureOr<void> _onGetItems(
      GetOthersItemsEvent event, Emitter<OthersState> emit) async {
    emit(state.copyWith(status: const BlocStatus.loading()));
    final result = await repository.getItems(categoryId: event.categoryId);
    result.fold(
      (error) => emit(state.copyWith(
          status: BlocStatus.failure(error), errorMessage: error.message)),
      (data) => emit(
          state.copyWith(status: const BlocStatus.success(), items: data)),
    );
  }
}
