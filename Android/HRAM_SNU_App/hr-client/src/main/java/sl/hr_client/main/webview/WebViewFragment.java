package sl.hr_client.main.webview;

import android.content.Context;
import android.os.Bundle;
import android.support.annotation.Nullable;
import android.view.KeyEvent;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.webkit.WebChromeClient;
import android.webkit.WebSettings;
import android.webkit.WebSettings.LayoutAlgorithm;
import android.webkit.WebView;
import android.webkit.WebViewClient;
import android.widget.TextView;

import sl.base.utils.UtilsToast;
import sl.hr_client.R;
import sl.hr_client.base.BaseFragment;
import sl.hr_client.utils.constant.TransDefine;

/**
 * Created by Administrator on 2017/4/24.
 */

public class WebViewFragment extends BaseFragment {
    public static final String WEB_VIEW = "WEB_VIEW";

    private Context ctx;

    private View webView;

    private String targetURL;
    private String title;
    private WebView wv;

    private TextView tvTitle;
    private TextView tvClose;

    @Nullable
    @Override
    public View onCreateView(LayoutInflater inflater, @Nullable ViewGroup container, @Nullable Bundle savedInstanceState) {
        webView = inflater.inflate(R.layout.fragment_webview, container, false);

        ctx = webView.getContext();
        receiveData();
        initView();
        return webView;
    }

    private void receiveData() {
        targetURL = getArguments().getString(TransDefine.Bundle_Link_URL);
        title = getArguments().getString(TransDefine.Bundle_Link_Title);

        if (targetURL == null) {
            UtilsToast.showToast(ctx, getString(R.string.cant_get_url));
            funcBack();
        }

        if (title == null) {
            title = getString(R.string.null_value);
        }
    }

    private void initView() {
        tvTitle = (TextView) webView.findViewById(R.id.tv_head);
        tvClose = (TextView) webView.findViewById(R.id.tv_head_right);
        wv = (WebView) webView.findViewById(R.id.wv);


        tvTitle.setText(title);
        tvClose.setVisibility(View.VISIBLE);
        tvClose.setBackgroundResource(R.mipmap.ico_close);

        wv.setOnKeyListener(new View.OnKeyListener() {
            @Override
            public boolean onKey(View v, int keyCode, KeyEvent event) {
                if (keyCode == KeyEvent.KEYCODE_BACK
                        && event.getAction() == KeyEvent.ACTION_UP) {
                    getFocus();
                    return true;
                }
                return false;
            }
        });


        tvClose.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                funcBack();
            }
        });


        wv.getSettings().setJavaScriptEnabled(true);
        wv.getSettings().setJavaScriptCanOpenWindowsAutomatically(true);
        wv.getSettings().setUseWideViewPort(true);//关键点
        wv.getSettings().setLayoutAlgorithm(LayoutAlgorithm.SINGLE_COLUMN);
        wv.getSettings().setDisplayZoomControls(false);
        wv.getSettings().setJavaScriptEnabled(true); // 设置支持javascript脚本
        wv.getSettings().setAllowFileAccess(true); // 允许访问文件
        wv.getSettings().setBuiltInZoomControls(true); // 设置显示缩放按钮
        wv.getSettings().setSupportZoom(true); // 支持缩放
        wv.getSettings().setLoadWithOverviewMode(true);
        wv.getSettings().setLayoutAlgorithm(LayoutAlgorithm.NARROW_COLUMNS);
        wv.getSettings().setSupportMultipleWindows(true);
        wv.setWebViewClient(new WebViewClient());
        wv.setWebChromeClient(new WebChromeClient());

        wv.loadUrl(targetURL);
    }

    private void funcBack() {
        getFragmentManager().popBackStack();
    }

    public void onResume() {
        super.onResume();
        getFocus();
    }

    //主界面获取焦点
    private void getFocus() {
        getView().setFocusableInTouchMode(true);
        getView().requestFocus();
        getView().setOnKeyListener(new View.OnKeyListener() {
            @Override
            public boolean onKey(View v, int keyCode, KeyEvent event) {
                if (event.getAction() == KeyEvent.ACTION_UP && keyCode == KeyEvent.KEYCODE_BACK) {
                    // 监听到返回按钮点击事件
                    if (wv.canGoBack()) {
                        wv.goBack();//返回上一页面
                        return true;
                    } else {
                        funcBack();
                    }
                    return true;
                }
                return false;
            }
        });
    }

}
