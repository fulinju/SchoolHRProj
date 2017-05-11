package sl.hr_client.main.webview;

import android.app.Activity;
import android.content.Context;
import android.graphics.Bitmap;
import android.os.Bundle;
import android.support.annotation.Nullable;
import android.support.v4.app.Fragment;
import android.view.KeyEvent;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.webkit.WebChromeClient;
import android.webkit.WebResourceError;
import android.webkit.WebResourceRequest;
import android.webkit.WebSettings;
import android.webkit.WebSettings.LayoutAlgorithm;
import android.webkit.WebView;
import android.webkit.WebViewClient;
import android.widget.ImageView;
import android.widget.RelativeLayout;
import android.widget.TextView;

import org.greenrobot.eventbus.EventBus;

import sl.base.ui.loading.AVLoadingIndicatorView;
import sl.base.ui.progressbar.HorizontalProgressBarWithNumber;
import sl.base.utils.UtilsToast;
import sl.hr_client.R;
import sl.hr_client.base.BaseFragment;
import sl.hr_client.event.TransferEvent;
import sl.hr_client.imp.FragmentBackListener;
import sl.hr_client.main.MainActivity;
import sl.hr_client.utils.constant.TransDefine;

/**
 * Created by Administrator on 2017/4/24.
 */

public class WebViewFragment extends BaseFragment implements View.OnClickListener, FragmentBackListener {
    public static final String WEB_VIEW = "WEB_VIEW";

    private Context ctx;

    private View webView;

    private String targetURL;
    private String title;

    private String shouldResetListener;

    private WebView wv;

    private TextView tvTitle;
    private TextView tvClose;

    private ImageView ivPre;
    private ImageView ivNext;
    private ImageView ivRefresh;

    //加载处理
    private RelativeLayout rlLoading; //加载区域
    private ImageView ivLoadFailed; //加载失败
    private AVLoadingIndicatorView vLoading;//加载中
    private TextView tvLoadState; //加载文字

    private HorizontalProgressBarWithNumber hpbLoading;


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
        shouldResetListener = getArguments().getString(TransDefine.Bundle_Should_Reset_Back_Listener)
                == null ? getString(R.string.null_value) : getArguments().getString(TransDefine.Bundle_Should_Reset_Back_Listener);

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
        ivPre = (ImageView) webView.findViewById(R.id.iv_pre);
        ivNext = (ImageView) webView.findViewById(R.id.iv_next);
        ivRefresh = (ImageView) webView.findViewById(R.id.iv_refresh);

        rlLoading = (RelativeLayout) webView.findViewById(R.id.rl_loading);
        ivLoadFailed = (ImageView) webView.findViewById(R.id.iv_load_failed);
        vLoading = (AVLoadingIndicatorView) webView.findViewById(R.id.v_loading);
        tvLoadState = (TextView) webView.findViewById(R.id.tv_load_state);
        hpbLoading = (HorizontalProgressBarWithNumber) webView.findViewById(R.id.hpb_loading);

        tvTitle.setText(title);
        tvClose.setVisibility(View.VISIBLE);
        tvClose.setBackgroundResource(R.mipmap.ico_close);

//        wv.setOnKeyListener(new View.OnKeyListener() {
//            @Override
//            public boolean onKey(View v, int keyCode, KeyEvent event) {
//                if (keyCode == KeyEvent.KEYCODE_BACK
//                        && event.getAction() == KeyEvent.ACTION_UP) {
//                    getFocus();
//                    return true;
//                }
//                return false;
//            }
//        });

        addListener();

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
        wv.setWebChromeClient(new WebChromeClient() {

            @Override
            public void onProgressChanged(WebView view, int newProgress) {
                if (newProgress < 100) {
                    hpbLoading.setProgress(newProgress);
                } else {
                    hpbLoading.setVisibility(View.GONE);
                }
            }
        });

        wv.setWebViewClient(new WebViewClient() {
            @Override
            public void onReceivedError(WebView view, WebResourceRequest request, WebResourceError error) {
                super.onReceivedError(view, request, error);
                showLoadingFailed();
            }

            @Override
            public void onPageFinished(WebView view, String url) {
                super.onPageFinished(view, url);
                hideLoading();

                if (wv.canGoBack()) {
                    ivPre.setImageResource(R.drawable.button_web_left);
                } else {
                    ivPre.setImageResource(R.mipmap.ico_left_pressed);
                }

                if (wv.canGoForward()) {
                    ivNext.setImageResource(R.drawable.button_web_right);
                } else {
                    ivNext.setImageResource(R.mipmap.ico_right_pressed);
                }
            }

            @Override
            public void onPageStarted(WebView view, String url, Bitmap favicon) {
                super.onPageStarted(view, url, favicon);
                showLoading();
            }

        });

        wv.loadUrl(targetURL);
    }

    private void addListener() {
        tvClose.setOnClickListener(this);
        ivPre.setOnClickListener(this);
        ivNext.setOnClickListener(this);
        ivRefresh.setOnClickListener(this);
    }

    @Override
    public void onClick(View view) {
        switch (view.getId()) {
            case R.id.tv_head_right:
                funcBack();
                break;
            case R.id.iv_pre:
                funcGoPre();
                break;
            case R.id.iv_next:
                funcGoNext();
                break;
            case R.id.iv_refresh:
                funcRefresh();
            default:
                break;
        }
    }

    private void funcBack() {
        getFragmentManager().popBackStack();
    }

    private void funcGoPre() {
        if (wv.canGoBack()) {
            wv.goBack();//返回上一页面
        }
    }

    private void funcGoNext() {
        if (wv.canGoForward()) {
            wv.goForward();//返回上一页面
        }
    }

    private void funcRefresh() {
        wv.reload();
    }

//    public void onResume() {
//        super.onResume();
//        getFocus();
//    }
//
//    //主界面获取焦点
//    private void getFocus() {
//        getView().setFocusableInTouchMode(true);
//        getView().requestFocus();
//        getView().setOnKeyListener(new View.OnKeyListener() {
//            @Override
//            public boolean onKey(View v, int keyCode, KeyEvent event) {
//                if (event.getAction() == KeyEvent.ACTION_UP && keyCode == KeyEvent.KEYCODE_BACK) {
//                    EventBus.getDefault().post(
//                            new TransferEvent(TransDefine.EVENT_CAPTURE_PRESS_BACK_KEY)); //发送截获返回键消息
//                    // 监听到返回按钮点击事件
//                    if (wv.canGoBack()) {
//                        wv.goBack();//返回上一页面
//                    } else {
//                        funcBack();
//                    }
//                    return true;
//
//                }
//                return false;
//            }
//        });
//    }


    public void showLoading() {
        hpbLoading.setVisibility(View.VISIBLE);
    }

    public void showLoadingFailed() {
        hpbLoading.setVisibility(View.GONE);
        rlLoading.setVisibility(View.VISIBLE);
        ivLoadFailed.setVisibility(View.VISIBLE);
        vLoading.setVisibility(View.GONE);
        tvLoadState.setText(ctx.getString(R.string.load_failed));
    }

    public void hideLoading() {
        rlLoading.setVisibility(View.GONE);
        hpbLoading.setVisibility(View.GONE);
    }

    @Override
    public void onAttach(Context context) {
        super.onAttach(context);
        if (context instanceof MainActivity) {
            ((MainActivity) context).setBackListener(this);
            ((MainActivity) context).setInterception(true);
        }


    }

    @Override
    public void onDetach() {
        super.onDetach();
        if (getActivity() instanceof MainActivity) {
            ((MainActivity) getActivity()).setBackListener(null);
            ((MainActivity) getActivity()).setInterception(false);
        }

        if (shouldResetListener.equals(TransDefine.Bundle_Should_Reset_Back_Listener_True)) {
            EventBus.getDefault().post(
                    new TransferEvent(TransDefine.EVENT_RESET_LINK_PRESS_BACK_LISTENER)); //重置友情链接的返回键监听
        }

    }

    @Override
    public void onBackForward() {
        if (wv.canGoBack()) {
            wv.goBack();//返回上一页面
        } else {
            funcBack();
        }
    }
}
