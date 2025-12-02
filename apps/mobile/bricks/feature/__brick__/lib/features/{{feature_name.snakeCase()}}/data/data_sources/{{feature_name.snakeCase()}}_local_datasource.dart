import 'package:core/core.dart';
import 'package:mobile/services/hive_service.dart';
import 'package:injectable/injectable.dart';


import '../../../../core/config/constants/hive_key.dart';

@injectable
class {{feature_name.pascalCase()}}LocaleDataSource {
  {{feature_name.pascalCase()}}LocaleDataSource({required this.hiveService});

  final HiveService hiveService;

  Future<void> SetLocally(String value) async {
    return throwAppException(() async {
      await HiveService.hive.put(HiveKey.user,value);
    });
  }
   Future<void> GetLocally() async {
    return throwAppException(() async {
      await HiveService.hive.get(HiveKey.user);
    });
  }
}
