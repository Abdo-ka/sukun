part of 'others_bloc.dart';

@immutable
class OthersState extends Equatable {
  final BlocStatus status;
  final List<OtherCategoryEntity> categories;
  final List<OtherItemEntity> items;
  final String? errorMessage;

  const OthersState({
    this.status = const BlocStatus.initial(),
    this.categories = const [],
    this.items = const [],
    this.errorMessage,
  });

  OthersState copyWith({
    BlocStatus? status,
    List<OtherCategoryEntity>? categories,
    List<OtherItemEntity>? items,
    String? errorMessage,
  }) {
    return OthersState(
      status: status ?? this.status,
      categories: categories ?? this.categories,
      items: items ?? this.items,
      errorMessage: errorMessage ?? this.errorMessage,
    );
  }

  @override
  List<Object?> get props => [status, categories, items, errorMessage];
}
