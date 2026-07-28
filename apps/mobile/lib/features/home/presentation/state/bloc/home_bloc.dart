// 🎯 Dart imports:
import 'dart:async';

 
import 'package:bloc/bloc.dart';
import 'package:equatable/equatable.dart';
import 'package:injectable/injectable.dart';
import 'package:meta/meta.dart';

 
import '../../../domain/repositories/home_repository.dart';

part 'home_event.dart';
part 'home_state.dart';

@injectable
class HomeBloc extends Bloc<HomeEvent, HomeState> {
final HomeRepository homeRepository;

  HomeBloc(this.homeRepository) : super(HomeState()) {
    on<HomeEvent>(_homeEvent);
  }
  FutureOr<void>_homeEvent(HomeEvent event, Emitter<HomeState> emit){
    
  }
}
