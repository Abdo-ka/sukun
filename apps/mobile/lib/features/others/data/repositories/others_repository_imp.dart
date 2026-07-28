import 'package:core/core.dart';
import 'package:injectable/injectable.dart';
import 'package:mobile/features/others/data/data_sources/others_local_datasource.dart';
import 'package:mobile/features/others/domain/entities/other_entity.dart';
import 'package:mobile/features/others/domain/repositories/others_repository.dart';

@Injectable(as: OthersRepository)
class OthersRepositoryImp implements OthersRepository {
  final OthersLocalDataSource dataSource;

  OthersRepositoryImp({required this.dataSource});

  @override
  FutureResult<List<OtherCategoryEntity>> getCategories() async {
    try {
      final result = await dataSource.getCategories();
      return Right(result);
    } catch (e) {
      return Left(AppException.unknown(exception: e, message: e.toString()));
    }
  }

  @override
  FutureResult<List<OtherItemEntity>> getItems({required int categoryId}) async {
    try {
      final result = await dataSource.getItems();
       // In a real app, filter by categoryId or fetch from API
      return Right(result);
    } catch (e) {
      return Left(AppException.unknown(exception: e, message: e.toString()));
    }
  }
}
