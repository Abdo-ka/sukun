import 'package:core/core.dart';

import 'package:mobile/features/home/domain/entities/home_entity.dart';

abstract class HomeRepository {
  FutureResult<String> getFromBackend({
    required HomeEntity param,
  });
}
