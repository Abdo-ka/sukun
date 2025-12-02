// 🐦 Flutter imports:
import 'package:flutter/material.dart';

// 📦 Package imports:
import 'package:fluttertoast/fluttertoast.dart';

class AppToasts {
  static void showFailureMessage(
    String? message, {
    Toast timeShowing = Toast.LENGTH_LONG,
  }) {
    Fluttertoast.cancel().then((value) => Fluttertoast.showToast(
          msg: message ?? 'Something went wrong!',
          backgroundColor: const Color.fromARGB(255, 190, 47, 37),
          textColor: Colors.white,
          toastLength: timeShowing,
          gravity: ToastGravity.BOTTOM,
        ));
  }

  static void showSuccessMessage(
    String? message, {
    Toast timeShowing = Toast.LENGTH_LONG,
  }) {
    Fluttertoast.cancel().then((value) => Fluttertoast.showToast(
          msg: message ?? 'Success!',
          backgroundColor: const Color.fromARGB(255, 0, 128, 0),
          textColor: Colors.white,
          toastLength: timeShowing,
          gravity: ToastGravity.BOTTOM,
        ));
  }
}
