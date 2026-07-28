// 🌎 Project imports:
import 'app_exception.dart';

typedef DataConverter<T> = T Function(dynamic json);

class BaseResponsePaginated<T> {
  final bool success;
  final int code;
  final String message;
  final int pageNumber;
  final int totalPages;
  final int totalDataCount;
  final T? data;

  BaseResponsePaginated({
    required this.data,
    required this.message,
    required this.code,
    required this.success,
    required this.pageNumber,
    required this.totalPages,
    required this.totalDataCount,
  });

  factory BaseResponsePaginated.fromJson(
      Map<String, dynamic> json, DataConverter<T> dataConverter) {
    if (!json['success']) {
      throw AppNetworkResponseException(message: json["message"]);
    }

    return BaseResponsePaginated(
      message: json["message"],
      code: json["code"],
      success: json["success"],
      pageNumber: json["pageNumber"],
      totalPages: json["totalPages"],
      totalDataCount: json["totalDataCount"],
      data: dataConverter(json["data"]),
    );
  }
}
