package sl.hr_client.main;

import android.animation.Animator;
import android.content.BroadcastReceiver;
import android.content.ComponentName;
import android.content.Context;
import android.content.Intent;
import android.content.res.Configuration;
import android.graphics.Color;
import android.graphics.drawable.BitmapDrawable;
import android.net.ConnectivityManager;
import android.net.NetworkInfo;
import android.os.Build;
import android.os.Bundle;
import android.provider.Settings;
import android.support.v4.app.FragmentManager;
import android.support.v4.app.FragmentTransaction;
import android.support.v4.widget.DrawerLayout;
import android.support.v7.app.ActionBar;
import android.support.v7.app.ActionBarDrawerToggle;
import android.support.v7.widget.Toolbar;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.view.animation.AccelerateInterpolator;
import android.widget.LinearLayout;
import android.widget.Toast;

import com.sl.lib.ui.circularreveal.animation.ViewAnimationUtils;
import com.sl.lib.ui.sidemenu.interfaces.ResourceAble;
import com.sl.lib.ui.sidemenu.interfaces.ScreenShotAble;
import com.sl.lib.ui.sidemenu.model.SlideMenuItem;
import com.sl.lib.ui.sidemenu.util.ViewAnimator;

import org.greenrobot.eventbus.EventBus;
import org.greenrobot.eventbus.Subscribe;
import org.greenrobot.eventbus.ThreadMode;

import java.util.ArrayList;
import java.util.List;

import sl.base.utils.UtilsNet;
import sl.base.utils.UtilsPreference;
import sl.base.utils.UtilsToast;
import sl.hr_client.R;
import sl.hr_client.base.BaseActivity;
import sl.hr_client.base.ContentFragment;
import sl.hr_client.data.DataUtils;
import sl.hr_client.event.TransferEvent;
import sl.hr_client.main.download.DownloadFragment;
import sl.hr_client.main.link.LinkFragment;
import sl.hr_client.main.member.MemberFragment;
import sl.hr_client.main.news.fragment.NewsFragment;
import sl.hr_client.main.setting.AccountFragment;
import sl.hr_client.utils.constant.ConstantData;
import sl.hr_client.utils.constant.TransDefine;

import static com.igexin.sdk.GTServiceManager.context;

/**
 * Created by xuzhijix on 2017/2/25.
 * 主页面
 */
public class MainActivity extends BaseActivity implements ViewAnimator.ViewAnimatorListener {
    private Context ctx;
    private ActionBar actionBar;

    private DrawerLayout drawerLayout;
    private ActionBarDrawerToggle drawerToggle;
    private List<SlideMenuItem> list = new ArrayList<>();
    private ViewAnimator viewAnimator;
    private LinearLayout leftDrawer;
    private LinearLayout llNoNet;


    private FragmentTransaction fragmentTransaction;  //用到次数多 获取一个

    private NewsFragment newsFragment;
    private DownloadFragment downloadFragment;
    private LinkFragment linkFragment;
    private MemberFragment memberFragment;
    private AccountFragment accountFragment;


    @Override
    protected void onNewIntent(Intent intent) {
        super.onNewIntent(intent);
        setIntent(intent);
        if (intent != null) {
//            boolean isReset = intent.getBooleanExtra(TransDefine.RESET_MAIN, false);
//            if (isReset) {
//                this.finish();
//                startActivity(getIntent());
//            }

            //退出相关
            boolean isExit = intent.getBooleanExtra(TransDefine.EXIT_APP, false); //双击退出App
            if (isExit) {
                this.finish();
            }
        }
    }

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        if (savedInstanceState == null) {
            setContentView(R.layout.activity_main);
        }

        ctx = this;
        //注册EventBus
        if (!EventBus.getDefault().isRegistered(this)) {
            EventBus.getDefault().register(this);
        }

        fragmentTransaction = getSupportFragmentManager().beginTransaction();

        drawerLayout = (DrawerLayout) findViewById(R.id.drawer_layout);
        llNoNet = (LinearLayout) findViewById(R.id.ll_no_net);

        drawerLayout.setScrimColor(Color.TRANSPARENT);
        leftDrawer = (LinearLayout) findViewById(R.id.left_drawer);
        leftDrawer.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                drawerLayout.closeDrawers();
            }
        });

        llNoNet.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                setNetwork();
            }
        });

        initFragment();

        setActionBar();
        createMenuList();

        funcJudgeNet();
    }

    private void setNetwork() {
        Intent intent = new Intent(Settings.ACTION_SETTINGS);
        startActivity(intent);
    }

    private void initFragment() {
        newsFragment = new NewsFragment();
        downloadFragment = new DownloadFragment();
        linkFragment = new LinkFragment();
        memberFragment = new MemberFragment();
        accountFragment = new AccountFragment();

        fragmentTransaction
                .add(R.id.content_frame, newsFragment, NewsFragment.NEWS)
                .add(R.id.content_frame, downloadFragment, DownloadFragment.DOWNLOADS)
                .add(R.id.content_frame, linkFragment, LinkFragment.LINKS)
                .add(R.id.content_frame, memberFragment, MemberFragment.MEMBERS)
                .add(R.id.content_frame, accountFragment, AccountFragment.ACCOUNT)
                .show(newsFragment).commit();

        viewAnimator = new ViewAnimator<>(this, list, newsFragment, drawerLayout, this);
    }

    private void createMenuList() {
        SlideMenuItem closeItem = new SlideMenuItem(ContentFragment.CLOSE, R.mipmap.icn_close);
        list.add(closeItem);
        SlideMenuItem newsItem = new SlideMenuItem(NewsFragment.NEWS, R.mipmap.icn_1);
        list.add(newsItem);
        SlideMenuItem downloadItem = new SlideMenuItem(DownloadFragment.DOWNLOADS, R.mipmap.icn_2);
        list.add(downloadItem);
        SlideMenuItem linksItem = new SlideMenuItem(LinkFragment.LINKS, R.mipmap.icn_3);
        list.add(linksItem);
        SlideMenuItem membersItem = new SlideMenuItem(MemberFragment.MEMBERS, R.mipmap.icn_4);
        list.add(membersItem);
        SlideMenuItem accountItem = new SlideMenuItem(AccountFragment.ACCOUNT, R.mipmap.ico_account);
        list.add(accountItem);
        SlideMenuItem exitItem = new SlideMenuItem(ContentFragment.EXIT, R.mipmap.ico_exit);
        list.add(exitItem);
    }


    private void setActionBar() {
        Toolbar toolbar = (Toolbar) findViewById(R.id.toolbar);
        setSupportActionBar(toolbar);

        actionBar = getSupportActionBar();

        getSupportActionBar().setHomeButtonEnabled(true);
        getSupportActionBar().setDisplayHomeAsUpEnabled(true);
        drawerToggle = new ActionBarDrawerToggle(
                this,                  /* host Activity */
                drawerLayout,         /* DrawerLayout object */
                toolbar,  /* nav drawer icon to replace 'Up' caret */
                R.string.drawer_open,  /* "open drawer" description */
                R.string.drawer_close  /* "close drawer" description */
        ) {

            /** Called when a drawer has settled in a completely closed state. */
            public void onDrawerClosed(View view) {
                super.onDrawerClosed(view);
                leftDrawer.removeAllViews();
                leftDrawer.invalidate();
            }

            @Override
            public void onDrawerSlide(View drawerView, float slideOffset) {
                super.onDrawerSlide(drawerView, slideOffset);
                if (slideOffset > 0.6 && leftDrawer.getChildCount() == 0)
                    viewAnimator.showMenuContent();
            }

            /** Called when a drawer has settled in a completely open state. */
            public void onDrawerOpened(View drawerView) {
                super.onDrawerOpened(drawerView);
            }
        };
        drawerLayout.addDrawerListener(drawerToggle);
    }

    @Override
    protected void onPostCreate(Bundle savedInstanceState) {
        super.onPostCreate(savedInstanceState);
        drawerToggle.syncState();
    }

    @Override
    public void onConfigurationChanged(Configuration newConfig) {
        super.onConfigurationChanged(newConfig);
        drawerToggle.onConfigurationChanged(newConfig);
    }


    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        getMenuInflater().inflate(R.menu.menu_main, menu);
        return true;
    }

    @Override
    public boolean onOptionsItemSelected(MenuItem item) {
        if (drawerToggle.onOptionsItemSelected(item)) {
            return true;
        }
        switch (item.getItemId()) {
            case R.id.action_settings:
                UtilsPreference.commitBoolean(ConstantData.FLAG_FIRST_OPEN, false);
                return true;
            default:
                return super.onOptionsItemSelected(item);
        }
    }

//    @Override
//    protected void onSaveInstanceState(Bundle outState) {
//        //super.onSaveInstanceState(outState);
//    }


    @Override
    public ScreenShotAble onSwitch(ResourceAble slideMenuItem, ScreenShotAble screenShotAble, int position) {
        switch (slideMenuItem.getName()) {
            case ContentFragment.CLOSE:
                return screenShotAble;
            case NewsFragment.NEWS:
                actionBar.setTitle(getString(R.string.app_name));
                return replaceFragment(newsFragment, screenShotAble, position, NewsFragment.NEWS);
            case DownloadFragment.DOWNLOADS:
                actionBar.setTitle(getString(R.string.downloads));
                return replaceFragment(downloadFragment, screenShotAble, position, DownloadFragment.DOWNLOADS);
            case LinkFragment.LINKS:
                actionBar.setTitle(getString(R.string.friendly_link));
                return replaceFragment(linkFragment, screenShotAble, position, LinkFragment.LINKS);
            case MemberFragment.MEMBERS:
                actionBar.setTitle(getString(R.string.member));
                return replaceFragment(memberFragment, screenShotAble, position, MemberFragment.MEMBERS);
            case AccountFragment.ACCOUNT:
                actionBar.setTitle(getString(R.string.account));
                return replaceFragment(accountFragment, screenShotAble, position, AccountFragment.ACCOUNT);
            case ContentFragment.EXIT:
                finish();
                return screenShotAble;
            default:
                return screenShotAble;
        }
    }

    //    @TargetApi(Build.VERSION_CODES.JELLY_BEAN)
    private ScreenShotAble replaceFragment(ContentFragment targetFragment, ScreenShotAble screenShotAble, int position, String tag) {
        startAnim(screenShotAble, position);
//        getSupportFragmentManager().beginTransaction()
        FragmentManager fragmentManager = getSupportFragmentManager();
        fragmentManager.beginTransaction()
                .replace(R.id.content_frame, targetFragment).commit(); //Replace会创建多个对象，但是Add在onPause后就销毁该引用了
//        List<Fragment> fragments = fragmentManager.getFragments();
//        for (int i = 0; i < fragments.size(); i++) {
//            if (fragments.get(i).isAdded()) {
//                getSupportFragmentManager().beginTransaction()
//                        .hide(fragments.get(i))
//                        .commitAllowingStateLoss();
//            }
//        }


//        switch (tag) {
//            case NewsFragment.NEWS:
//                fragmentManager.beginTransaction()
//                        .show(newsFragment)
//                        .hide(downloadFragment)
//                        .hide(linkFragment)
//                        .hide(memberFragment)
//                        .commit();
//                break;
//            case DownloadFragment.DOWNLOADS:
//                fragmentManager.beginTransaction()
//                        .hide(newsFragment)
//                        .show(downloadFragment)
//                        .hide(linkFragment)
//                        .hide(memberFragment)
//                        .commit();
//                break;
//            case LinkFragment.LINKS:
//                fragmentManager.beginTransaction()
//                        .hide(newsFragment)
//                        .hide(downloadFragment)
//                        .show(linkFragment)
//                        .hide(memberFragment)
//                        .commit();
//                break;
//            case MemberFragment.MEMBERS:
//                fragmentManager.beginTransaction()
//                        .hide(newsFragment)
//                        .hide(downloadFragment)
//                        .hide(linkFragment)
//                        .show(memberFragment)
//                        .commit();
//                break;
//            default:
//                break;
//        }

        return targetFragment;
    }


    private void startAnim(ScreenShotAble screenShotAble, int position) {
        View view = findViewById(R.id.content_frame);
        int finalRadius = Math.max(view.getWidth(), view.getHeight());
        Animator animator = null;
        animator = ViewAnimationUtils.createCircularReveal(view, 0, position, 0, finalRadius);

        animator.setInterpolator(new AccelerateInterpolator());
        animator.setDuration(ViewAnimator.CIRCULAR_REVEAL_ANIMATION_DURATION);

        if (Build.VERSION.SDK_INT >= Build.VERSION_CODES.JELLY_BEAN) {
            findViewById(R.id.content_overlay).setBackground(new BitmapDrawable(getResources(), screenShotAble.getBitmap()));
        } else {
            findViewById(R.id.content_overlay).setBackgroundDrawable(new BitmapDrawable(getResources(), screenShotAble.getBitmap()));
        }
        animator.start();
    }

    @Override
    public void disableHomeButton() {
        getSupportActionBar().setHomeButtonEnabled(false);

    }

    @Override
    public void enableHomeButton() {
        getSupportActionBar().setHomeButtonEnabled(true);
        drawerLayout.closeDrawers();
    }



    @Override
    public void addViewToContainer(View view) {
        leftDrawer.addView(view);
    }

    @Subscribe(threadMode = ThreadMode.MAIN)
    public void onEventNetChange(TransferEvent event) {
        if (event.getTargetTag().equals(TransDefine.EVENT_MODIFY_NET)) {
            funcJudgeNet();
        }
    }

    private void funcJudgeNet() {
        if (UtilsNet.isNetworkAvailable(ctx) == true) {
            llNoNet.setVisibility(View.GONE);
        } else {
            llNoNet.setVisibility(View.VISIBLE);
        }
    }

    @Override
    public void onDestroy() {
        super.onDestroy();
        EventBus.getDefault().unregister(this);
    }
}
