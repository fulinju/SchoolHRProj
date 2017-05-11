package sl.hr_client.base;

import android.content.res.Configuration;
import android.content.res.Resources;
import android.os.Bundle;
import android.support.v7.app.AppCompatActivity;
import android.util.DisplayMetrics;
import android.view.WindowManager;

import java.util.Locale;

import sl.base.utils.UtilsPreference;
import sl.hr_client.utils.constant.ConstantData;

/**
 * Created by xuzhijix on 2017/2/25.
 */
public class BaseActivity extends AppCompatActivity {
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        // 初始化PreferenceUtil
        UtilsPreference.init(this);
        // 根据上次的语言设置，重新设置语言
        switchLanguage(UtilsPreference.getString(ConstantData.languageKey, ConstantData.languageZH)); //默认中文

        if (UtilsPreference.getBoolean(ConstantData.FLAG_PROTECT_SCREEN, ConstantData.default_boolean) == true) {
            getWindow().setFlags(WindowManager.LayoutParams.FLAG_SECURE, WindowManager.LayoutParams.FLAG_SECURE);
        }

    }

    /**
     * <切换语言>
     *
     * @param language
     * @see [类、类#方法、类#成员]
     */
    protected void switchLanguage(String language) {
        // 设置应用语言类型
        Resources resources = getResources();
        Configuration config = resources.getConfiguration();
        DisplayMetrics dm = resources.getDisplayMetrics();
        if (language.equals(ConstantData.languageEN)) {
            config.locale = Locale.ENGLISH;
        } else {
            // 简体中文
            config.locale = Locale.SIMPLIFIED_CHINESE;
        }
        resources.updateConfiguration(config, dm);
        // 保存设置语言的类型
        UtilsPreference.commitString(ConstantData.languageKey, language);
    }
}
