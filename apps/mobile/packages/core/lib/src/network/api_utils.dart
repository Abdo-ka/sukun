import 'dart:async';
import 'dart:developer';
import 'dart:io';

import '../error/app_exception.dart';

Future<T> throwAppException<T>(FutureOr<T> Function() call) async {
  try {
    return (await call());
  } on AppException catch (e) {
    showMessage(e.message);
    rethrow;
  } on SocketException catch (e) {
    showMessage(e.message);
    throw AppNetworkException(
        message: e.message,
        reason: AppNetworkExceptionReason.noInternet,
        exception: e);
  } on Exception catch (e) {
    showMessage(e.toString());

    throw AppException.unknown(exception: e, message: e.toString());
  } catch (e, s) {
    showMessage(e.toString());

    log(e.toString(), stackTrace: s);
    throw AppException.unknown(exception: e, message: e.toString());
  }
}

void showMessage(String message, {bool isSuccess = false}) {
  // Fluttertoast.showToast(
  //     msg: message,
  //     toastLength: Toast.LENGTH_SHORT,
  //     gravity: ToastGravity.BOTTOM,
  //     timeInSecForIosWeb: 1,
  //     backgroundColor: isSuccess ? Colors.greenAccent : Colors.red,
  //     textColor: Colors.white,
  //     fontSize: 16.0);
  // Toast.show("Toast plugin app", duration: Toast.lengthShort, gravity:  Toast.bottom);
}
