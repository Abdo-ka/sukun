// 🌎 Project imports:
import 'app_exception.dart';

typedef DataConverter<T> = T Function(dynamic json);

class BaseResponse<T> {
  final bool success;
  final int code;
  final String message;
  final T? data;

  BaseResponse({
    required this.data,
    required this.message,
    required this.code,
    required this.success,
  });

  factory BaseResponse.fromJson(
      Map<String, dynamic> json, DataConverter<T> dataConverter) {
    if (!json['success']) {
      throw AppNetworkResponseException(message: json["message"]);
    }

    return BaseResponse(
      message: json["message"],
      code: json["code"],
      success: json["success"],
      data: dataConverter(json["data"]),
    );
  }
}
