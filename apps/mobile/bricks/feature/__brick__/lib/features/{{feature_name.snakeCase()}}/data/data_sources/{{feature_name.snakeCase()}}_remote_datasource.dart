import 'package:injectable/injectable.dart';
import 'package:dio/dio.dart';
import 'package:core/core.dart';


@injectable
class {{feature_name.pascalCase()}}RemoteDataSource {

{{feature_name.pascalCase()}}RemoteDataSource({required this.dio});
final DioClient dio;


FutureResult<String> get() async {
return throwAppException(() async {
final result = await dio.get("");
return result.data;
    });
  }
}
