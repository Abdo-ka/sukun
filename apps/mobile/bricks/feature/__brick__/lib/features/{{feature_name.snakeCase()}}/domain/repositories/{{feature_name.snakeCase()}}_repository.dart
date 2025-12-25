 import 'package:core/core.dart';

import 'package:mobile/features/{{feature_name.snakeCase()}}/domain/entities/{{feature_name.snakeCase()}}_entity.dart';

abstract class {{feature_name.pascalCase()}}Repository{
  
   FutureResult<String> getFromBackend({required {{feature_name.pascalCase()}}Entity param});
}