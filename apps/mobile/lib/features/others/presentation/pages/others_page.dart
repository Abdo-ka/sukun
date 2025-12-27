import 'package:auto_route/auto_route.dart';
import 'package:core/core.dart';
import 'package:flutter/material.dart';
import 'package:mobile/features/others/presentation/pages/mobile/others_page_mobile.dart';

@RoutePage()
class OthersPage extends StatelessWidget {
  const OthersPage({super.key});

  @override
  Widget build(BuildContext context) {
    //here same move app scaffold into OthersPageMobile instead of here
    return AppScaffold(
      body: PageLayoutBuilder(
        mobile: (context) => OthersPageMobile(),
      ),
    );
  }
}
