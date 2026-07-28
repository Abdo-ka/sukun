// 🎯 Dart imports:
import 'dart:async';

 
import 'package:bloc/bloc.dart';
import 'package:equatable/equatable.dart';
import 'package:injectable/injectable.dart';
import 'package:meta/meta.dart';

 
import '../../../domain/repositories/{{feature_name.snakeCase()}}_repository.dart';

part '{{feature_name.snakeCase()}}_event.dart';
part '{{feature_name.snakeCase()}}_state.dart';

@injectable
class {{feature_name.pascalCase()}}Bloc extends Bloc<{{feature_name.pascalCase()}}Event, {{feature_name.pascalCase()}}State> {
final {{feature_name.pascalCase()}}Repository {{feature_name.snakeCase()}}Repository;

  {{feature_name.pascalCase()}}Bloc(this.{{feature_name.snakeCase()}}Repository) : super({{feature_name.pascalCase()}}State()) {
    on<{{feature_name.pascalCase()}}Event>(_{{feature_name.snakeCase()}}Event);
  }
  FutureOr<void>_{{feature_name.snakeCase()}}Event({{feature_name.pascalCase()}}Event event, Emitter<{{feature_name.pascalCase()}}State> emit){
    
  }
}
