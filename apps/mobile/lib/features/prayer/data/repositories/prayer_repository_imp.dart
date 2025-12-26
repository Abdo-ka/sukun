import 'package:injectable/injectable.dart';
import '../../domain/repositories/prayer_repository.dart';
import 'package:core/core.dart';
import '../../domain/entities/prayer_entity.dart';
import '../../domain/repositories/prayer_repository.dart';
import '../data_sources/prayer_remote_datasource.dart';


@Injectable(as: PrayerRepository)
class PrayerRepositoryImp implements PrayerRepository {

final PrayerRemoteDataSource dataSource;
PrayerRepositoryImp({required this.dataSource});

@override
  FutureResult<String> getFromBackend({required PrayerEntity param})async => await dataSource.get();
}





