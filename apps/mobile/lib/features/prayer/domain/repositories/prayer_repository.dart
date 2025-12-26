 import 'package:core/core.dart';

import 'package:mobile/features/prayer/domain/entities/prayer_entity.dart';

abstract class PrayerRepository{
  
   FutureResult<String> getFromBackend({required PrayerEntity param});
}