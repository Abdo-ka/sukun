import 'package:core/core.dart';
import 'package:mobile/features/others/domain/entities/other_entity.dart';

abstract class OthersRepository {
  FutureResult<List<OtherCategoryEntity>> getCategories();
  FutureResult<List<OtherItemEntity>> getItems({required int categoryId});
}
