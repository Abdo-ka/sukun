// GENERATED CODE - DO NOT MODIFY BY HAND
// dart format width=80

// **************************************************************************
// InjectableConfigGenerator
// **************************************************************************

// ignore_for_file: type=lint
// coverage:ignore-file

// ignore_for_file: no_leading_underscores_for_library_prefixes
import 'package:core/core.dart' as _i494;
import 'package:get_it/get_it.dart' as _i174;
import 'package:injectable/injectable.dart' as _i526;
import 'package:mobile/core/di/di_container.dart' as _i304;
import 'package:mobile/core/repositories/local_storage.dart' as _i1029;
import 'package:mobile/core/repositories/token_repository.dart' as _i591;
import 'package:mobile/features/home/data/data_sources/home_local_datasource.dart'
    as _i176;
import 'package:mobile/features/home/data/data_sources/home_remote_datasource.dart'
    as _i231;
import 'package:mobile/features/home/data/repositories/home_repository_imp.dart'
    as _i247;
import 'package:mobile/features/home/domain/repositories/home_repository.dart'
    as _i54;
import 'package:mobile/features/home/presentation/state/bloc/home_bloc.dart'
    as _i726;
import 'package:mobile/services/hive_service.dart' as _i775;
import 'package:mobile/services/router/router.dart' as _i426;
import 'package:shared_preferences/shared_preferences.dart' as _i460;

extension GetItInjectableX on _i174.GetIt {
  // initializes the registration of main-scope dependencies inside of GetIt
  _i174.GetIt init({
    String? environment,
    _i526.EnvironmentFilter? environmentFilter,
  }) {
    final gh = _i526.GetItHelper(this, environment, environmentFilter);
    final appModule = _$AppModule();
    gh.singletonAsync<_i460.SharedPreferences>(
      () => appModule.sharedPreferences,
    );
    gh.singleton<_i426.AppRouter>(() => appModule.router);
    gh.lazySingleton<_i494.DioClient>(() => appModule.client);
    gh.lazySingleton<_i775.HiveService>(() => _i775.HiveService());
    gh.factory<_i591.TokenRepository>(() => _i591.TokenRepositoryImp());
    gh.factory<_i176.HomeLocaleDataSource>(
      () => _i176.HomeLocaleDataSource(hiveService: gh<_i775.HiveService>()),
    );
    gh.singletonAsync<_i1029.LocalStorage>(
      () async =>
          _i1029.LocalStorage(await getAsync<_i460.SharedPreferences>()),
    );
    gh.factory<_i231.HomeRemoteDataSource>(
      () => _i231.HomeRemoteDataSource(dio: gh<_i494.DioClient>()),
    );
    gh.factory<_i54.HomeRepository>(
      () =>
          _i247.HomeRepositoryImp(dataSource: gh<_i231.HomeRemoteDataSource>()),
    );
    gh.factory<_i726.HomeBloc>(() => _i726.HomeBloc(gh<_i54.HomeRepository>()));
    return this;
  }
}

class _$AppModule extends _i304.AppModule {}
