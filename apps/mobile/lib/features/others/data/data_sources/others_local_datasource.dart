import 'package:injectable/injectable.dart';
import 'package:mobile/gen/assets.gen.dart';
import '../../domain/entities/other_entity.dart';

abstract class OthersLocalDataSource {
  Future<List<OtherCategoryEntity>> getCategories();
  Future<List<OtherItemEntity>> getItems();
}

@LazySingleton(as: OthersLocalDataSource)
class OthersLocalDataSourceImp implements OthersLocalDataSource {
  @override
  Future<List<OtherCategoryEntity>> getCategories() async {
    return [
      OtherCategoryEntity(
        id: 1,
        title: 'أسماء الله الحسنى',
        icon: Assets.icons.allah,
        isSvg: true,
      ),
      OtherCategoryEntity(
        id: 2,
        title: 'سنن مهجورة',
        icon: Assets.icons.sunan,
        isSvg: true,
      ),
      OtherCategoryEntity(
        id: 3,
        title: 'علامات الساعة',
        icon: Assets.icons.saasMarks,
        isSvg: true,
      ),
      OtherCategoryEntity(
        id: 4,
        title: 'نساء في الاسلام',
        icon: Assets.icons.woman,
        isSvg: false,
      ),
      OtherCategoryEntity(
        id: 5,
        title: 'آية وعبرة',
        icon: Assets.icons.ayah,
        isSvg: true,
      ),
      OtherCategoryEntity(
        id: 6,
        title: 'الزكاة',
        icon: Assets.icons.zakat,
        isSvg: false,
      ),
      OtherCategoryEntity(
        id: 7,
        title: 'قصص منوعة',
        icon: Assets.icons.stories,
        isSvg: true,
      ),
    ];
  }

  @override
  Future<List<OtherItemEntity>> getItems() async {
    // Return dummy data for now
    return const [
       OtherItemEntity(id: 1, title: 'عائشة بنت أبي بكر رضي الله عنه', content: ''),
       OtherItemEntity(id: 2, title: 'رفيدة الأسلمية', content: ''),
       OtherItemEntity(id: 3, title: 'نسيبة بنت كعب المازنية', content: ''),
       OtherItemEntity(id: 4, title: 'الشفاء بنت عبد الله العدوية', content: ''),
       OtherItemEntity(id: 5, title: 'فاطمة بنت محمد رضي الله عنها', content: ''),
       OtherItemEntity(id: 6, title: 'خديجة بنت خويلد رضي الله عنها', content: ''),
       OtherItemEntity(id: 7, title: 'حفصة بنت عمر بن الخطاب', content: ''),
    ];
  }
}
