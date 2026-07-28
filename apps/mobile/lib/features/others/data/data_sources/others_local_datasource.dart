import 'package:injectable/injectable.dart';
import '../../domain/entities/other_entity.dart';
import '../../core/others_constants.dart';

abstract class OthersLocalDataSource {
  Future<List<OtherCategoryEntity>> getCategories();
  Future<List<OtherItemEntity>> getItems();
}

// Fixed: Added these into constants file (lib/features/others/core/others_constants.dart)
// Fixed: Changed LazySingleton to Injectable as requested
@Injectable(as: OthersLocalDataSource)
class OthersLocalDataSourceImp
    implements OthersLocalDataSource {
  @override
  Future<List<OtherCategoryEntity>> getCategories() async {
    return othersCategories;
  }

  // Fixed: Moving data to constants file as a first step. API integration can be added later.
  @override
  Future<List<OtherItemEntity>> getItems() async {
    return othersItems;
  }
}
