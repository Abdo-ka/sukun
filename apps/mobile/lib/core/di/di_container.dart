// 🎯 Dart imports:
import 'dart:async';

// 📦 Package imports:
import 'package:core/core.dart';
import 'package:dio/dio.dart';
import 'package:get_it/get_it.dart';
import 'package:injectable/injectable.dart';
// 🌎 Project imports:
import 'package:mobile/core/di/di_container.config.dart';
import 'package:shared_preferences/shared_preferences.dart';

import '../../services/router/router.dart';
import '../config/api_routes.dart';
import '../repositories/token_repository.dart';

final GetIt getIt = GetIt.instance;

@InjectableInit()
Future<GetIt> configureDependencies() async => getIt.init();

@module
abstract class AppModule {
  @singleton
  Future<SharedPreferences> get sharedPreferences =>
      SharedPreferences.getInstance();

  @singleton
  AppRouter get router => AppRouter();

  @lazySingleton
  DioClient get client => DioClient(
    baseUrl: ApiRoutes.baseUrl,
    interceptors: [
      InterceptorsWrapper(
        onRequest: (options, handler) async {
          final token = await TokenRepositoryImp().token;
          if (token != null) {
            options.headers['Authorization'] = token;
          }
          return handler.next(options);
        },
      ),
    ],
  );
}
