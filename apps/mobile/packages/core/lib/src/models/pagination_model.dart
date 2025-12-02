/// Immutable pagination model for API responses
/// Provides type-safe pagination data with efficient JSON parsing
class PaginationModel<T> {
  const PaginationModel({
    required this.pageNumber,
    required this.totalPages,
    required this.totalDataCount,
    required this.data,
  });

  final int pageNumber;
  final int totalPages;
  final int totalDataCount;
  final List<T> data;

  /// Check if this is the first page
  bool get isFirstPage => pageNumber == 0;

  /// Check if this is the last page
  bool get isLastPage => pageNumber >= totalPages - 1;

  /// Check if data is empty
  bool get isEmpty => data.isEmpty;

  /// Check if data is not empty
  bool get isNotEmpty => data.isNotEmpty;

  factory PaginationModel.fromJson(
    Map<String, dynamic> json,
    T Function(Object? json) tFromJson,
  ) {
    try {
      final dataList = json['data'];
      if (dataList is! List) {
        throw ArgumentError(
            'Expected List for data field but got ${dataList.runtimeType}');
      }

      return PaginationModel(
        pageNumber: (json['pageNumber'] as num?)?.toInt() ?? 0,
        totalPages: (json['totalPages'] as num?)?.toInt() ?? 0,
        totalDataCount: (json['totalDataCount'] as num?)?.toInt() ?? 0,
        data: dataList.map((e) => tFromJson(e)).toList(),
      );
    } catch (e) {
      throw FormatException('Failed to parse PaginationModel: $e');
    }
  }
}
