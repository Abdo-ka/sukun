import 'package:injectable/injectable.dart';
import '../../domain/repositories/home_repository.dart';
import 'package:core/core.dart';
import '../../domain/entities/home_entity.dart';
import '../../domain/repositories/home_repository.dart';
import '../data_sources/home_remote_datasource.dart';


@Injectable(as: HomeRepository)
class HomeRepositoryImp implements HomeRepository {

final HomeRemoteDataSource dataSource;
HomeRepositoryImp({required this.dataSource});

@override
  FutureResult<String> getFromBackend({required HomeEntity param})async => await dataSource.get();
}





