part of 'others_bloc.dart';

@immutable
abstract class OthersEvent extends Equatable {
  const OthersEvent();
}

class GetOthersCategoriesEvent extends OthersEvent {
  const GetOthersCategoriesEvent();
  @override
  List<Object?> get props => [];
}

class GetOthersItemsEvent extends OthersEvent {
  final int categoryId;
  const GetOthersItemsEvent({required this.categoryId});
   @override
  List<Object?> get props => [categoryId];
}
