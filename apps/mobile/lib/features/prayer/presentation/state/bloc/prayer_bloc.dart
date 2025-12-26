// 🎯 Dart imports:
import 'dart:async';

 
import 'package:bloc/bloc.dart';
import 'package:equatable/equatable.dart';
import 'package:injectable/injectable.dart';
import 'package:meta/meta.dart';

 
import '../../../domain/repositories/prayer_repository.dart';

part 'prayer_event.dart';
part 'prayer_state.dart';

@injectable
class PrayerBloc extends Bloc<PrayerEvent, PrayerState> {
final PrayerRepository prayerRepository;

  PrayerBloc(this.prayerRepository) : super(PrayerState()) {
    on<PrayerEvent>(_prayerEvent);
  }
  FutureOr<void>_prayerEvent(PrayerEvent event, Emitter<PrayerState> emit){
    
  }
}
