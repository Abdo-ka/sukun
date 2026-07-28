import 'package:flutter/material.dart';

import 'package:flutter_screenutil/flutter_screenutil.dart';

extension Extensions on num {
  Widget get ws => SizedBox(width: double.tryParse(toString())?.w);

  Widget get hs => SizedBox(height: double.tryParse(toString())?.h);
}
