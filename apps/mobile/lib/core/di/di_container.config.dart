// dart format width=80
// GENERATED CODE - DO NOT MODIFY BY HAND

// **************************************************************************
// InjectableConfigGenerator
// **************************************************************************

// ignore_for_file: type=lint
// coverage:ignore-file

// ignore_for_file: no_leading_underscores_for_library_prefixes
import 'package:core/core.dart' as _i494;
import 'package:get_it/get_it.dart' as _i174;
import 'package:injectable/injectable.dart' as _i526;
import 'package:mobile/core/di/di_container.dart' as _i145;
import 'package:mobile/core/repositories/local_storage.dart' as _i558;
import 'package:mobile/core/repositories/token_repository.dart' as _i378;
import 'package:mobile/services/router/router.dart' as _i590;
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
    gh.singleton<_i590.AppRouter>(() => appModule.router);
    gh.lazySingleton<_i494.DioClient>(() => appModule.client);
    gh.factory<_i378.TokenRepository>(() => _i378.TokenRepositoryImp());
    gh.singletonAsync<_i558.LocalStorage>(
      () async => _i558.LocalStorage(await getAsync<_i460.SharedPreferences>()),
    );
    return this;
  }
}

class _$AppModule extends _i145.AppModule {}
