import 'dart:convert';

import 'package:core/src/error/app_exception.dart';
import 'package:either_dart/either.dart';
import 'package:flutter/material.dart';
import 'package:infinite_scroll_pagination/infinite_scroll_pagination.dart';
import 'package:intl/intl.dart';

import '../models/pagination_model.dart';

/// Core utility functions for common operations
/// Includes API handling, pagination, date/time conversions, and type safety
class CoreHelperFunctions {
  // Private constructor to prevent instantiation
  CoreHelperFunctions._();

  /// Cast to [T] if possible or return null - type-safe casting
  static T? castOrNull<T>(dynamic x) => x is T ? x : null;

  static String timeOfDayToString(TimeOfDay time) =>
      '${time.hour.toString().padLeft(2, '0')}:${time.minute.toString().padLeft(2, '0')}:00';

  ///Converts from [TimeOfDay] to [DateTime]
  static DateTime timeOfDayToDateTime(
          TimeOfDay timeOfDay) =>
      DateTime(
        DateTime.now().year,
        DateTime.now().month,
        DateTime.now().day,
        timeOfDay.hour,
        timeOfDay.minute,
      );

  static bool hasReachedMax(
          {required int totalPage,
          required int pageNumber}) =>
      totalPage == 0
          ? totalPage == pageNumber
          : totalPage == pageNumber + 1;

  /// Appends new page to the given paging controller in case there is one,
  /// Appends last page in case there is no more,
  /// Appends error in case of failure
  static void managePaginationController<T>(
    Either<AppException, PaginationModel<T>> result,
    PagingController<int, T> controller,
    int pageNumber,
  ) {
    result.fold(
      (l) => controller.value =
          controller.value.copyWith(error: l),
      (r) {
        // check if it's the last page
        final isLastPage = hasReachedMax(
            totalPage: r.totalPages,
            pageNumber: r.pageNumber);

        if (r.pageNumber == 0) {
          // Reset for first page
          controller.value = PagingState(
            pages: [r.data],
            keys: [pageNumber],
            hasNextPage: !isLastPage,
            isLoading: false,
          );
        } else {
          // Append to existing pages
          controller.value = controller.value.copyWith(
            pages: [...?controller.pages, r.data],
            keys: [...?controller.keys, pageNumber],
            hasNextPage: !isLastPage,
          );
        }
      },
    );
  }

  // Cached date formatters for better performance
  static final _dateFormatter = DateFormat('yyyy-MM-dd');
  static final _timeFormatter = DateFormat.Hm();
  static final _dateTimeFormatter = DateFormat(
      "${DateFormat.DAY} ${DateFormat.MONTH} ${DateFormat.YEAR}");

  static DateTime fromStringToDateTime(
          String formattedString) =>
      DateTime.parse(formattedString);

  static String fromDateTimeToString(DateTime dateTime) =>
      _dateTimeFormatter.format(dateTime);

  static String fromTimeToString(DateTime dateTime) =>
      _timeFormatter.format(dateTime);

  static String? nullableFromDateToString(DateTime? date) =>
      date == null ? null : _dateFormatter.format(date);

  static DateTime? nullableFromStringToDateTime(
          String? formattedString) =>
      formattedString == null
          ? null
          : DateTime.parse(formattedString);

  static String? nullableFromDateTimeToString(
          DateTime? dateTime) =>
      dateTime?.toIso8601String();

  /// Convert JSON string to model with type safety
  static T convertStringToModel<T>(
    T Function(Map<String, dynamic> map) fromJson,
    String response,
  ) {
    final decoded = jsonDecode(response);
    if (decoded is! Map) {
      throw ArgumentError(
          'Expected Map but got ${decoded.runtimeType}');
    }
    return fromJson(decoded.cast<String, dynamic>());
  }

  /// Convert JSON list string to model list with type safety
  static List<T> convertStringListToModel<T>(
    T Function(Map<String, dynamic> map) fromJson,
    String response,
  ) {
    final decoded = jsonDecode(response);
    if (decoded is! List) {
      throw ArgumentError(
          'Expected List but got ${decoded.runtimeType}');
    }
    return decoded
        .whereType<Map>()
        .map((e) => fromJson(e.cast<String, dynamic>()))
        .toList();
  }

  /// Convert Map to model with type safety
  static T convertMapToModel<T>(
    T Function(Map<String, dynamic> map) fromJson,
    Map<dynamic, dynamic> response,
  ) {
    return fromJson(response.cast<String, dynamic>());
  }

  /// Convert List to model list with type safety
  static List<T> convertListToModel<T>(
    T Function(Map<String, dynamic> map) fromJson,
    List<dynamic> data,
  ) {
    return data
        .whereType<Map>()
        .map((e) => fromJson(e.cast<String, dynamic>()))
        .toList();
  }
}

mixin Helper<T> {}
