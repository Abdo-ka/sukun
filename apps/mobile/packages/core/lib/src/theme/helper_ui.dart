import 'package:flutter/material.dart';
import 'package:flutter_screenutil/flutter_screenutil.dart';

class HelperUI {
  static List<Widget> getChildren({
    required List<Widget> children,
    required double padding,
  }) {
    List<Widget> modifiedList = [];
    for (var i = 0; i < children.length; i++) {
      modifiedList.add(children[i]);
      if (i != children.length - 1) {
        modifiedList.add(padding.horizontalSpace);
      }
    }
    return modifiedList;
  }
}
