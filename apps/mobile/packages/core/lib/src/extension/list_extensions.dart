import 'dart:convert';

extension ListExtension<T> on List<T> {
  String toJson(Map<String, dynamic> Function(T instance) toJson) =>
      jsonEncode(map((e) => toJson(e)).toList());

  T setState(Enum stateEnum, T newState) => this[stateEnum.index] = newState;
}
