package sl.hr_client.main;

import android.animation.Animator;
import android.content.res.Configuration;
import android.graphics.Color;
import android.graphics.drawable.BitmapDrawable;
import android.os.Build;
import android.os.Bundle;
import android.support.v4.app.Fragment;
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

import com.sl.lib.ui.circularreveal.animation.ViewAnimationUtils;
import com.sl.lib.ui.sidemenu.interfaces.ResourceAble;
import com.sl.lib.ui.sidemenu.interfaces.ScreenShotAble;
import com.sl.lib.ui.sidemenu.model.SlideMenuItem;
import com.sl.lib.ui.sidemenu.util.ViewAnimator;

import java.util.ArrayList;
import java.util.List;

import sl.hr_client.R;
import sl.hr_client.base.BaseActivity;
import sl.hr_client.base.ContentFragment;
import sl.hr_client.main.download.DownloadFragment;
import sl.hr_client.main.link.LinkFragment;
import sl.hr_client.main.member.MemberFragment;
import sl.hr_client.main.news.fragment.NewsFragment;

/**
 * Created by xuzhijix on 2017/2/25.
 * 主页面
 */
public class MainActivity extends BaseActivity implements ViewAnimator.ViewAnimatorListener {
    private ActionBar actionBar;

    private DrawerLayout drawerLayout;
    private ActionBarDrawerToggle drawerToggle;
    private List<SlideMenuItem> list = new ArrayList<>();
    private ViewAnimator viewAnimator;
    private LinearLayout linearLayout;

    private FragmentTransaction fragmentTransaction;  //用到次数多 获取一个

    private NewsFragment newsFragment;
    private DownloadFragment downloadFragment;
    private LinkFragment linkFragment;
    private MemberFragment memberFragment;


    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        if (savedInstanceState == null) {
            setContentView(R.layout.activity_main);
        }


        fragmentTransaction = getSupportFragmentManager().beginTransaction();

        drawerLayout = (DrawerLayout) findViewById(R.id.drawer_layout);
        drawerLayout.setScrimColor(Color.TRANSPARENT);
        linearLayout = (LinearLayout) findViewById(R.id.left_drawer);
        linearLayout.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                drawerLayout.closeDrawers();
            }
        });

        initFragment();

        setActionBar();
        createMenuList();
    }

    private void initFragment() {
        newsFragment = new NewsFragment();
        downloadFragment = new DownloadFragment();
        linkFragment = new LinkFragment();
        memberFragment = new MemberFragment();

        fragmentTransaction
                .add(R.id.content_frame, newsFragment, NewsFragment.NEWS)
                .add(R.id.content_frame, downloadFragment, DownloadFragment.DOWNLOADS)
                .add(R.id.content_frame, linkFragment, LinkFragment.LINKS)
                .add(R.id.content_frame, memberFragment, MemberFragment.MEMBERS)
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
        SlideMenuItem exitItem = new SlideMenuItem(ContentFragment.EXIT, R.mipmap.icn_7);
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
                linearLayout.removeAllViews();
                linearLayout.invalidate();
            }

            @Override
            public void onDrawerSlide(View drawerView, float slideOffset) {
                super.onDrawerSlide(drawerView, slideOffset);
                if (slideOffset > 0.6 && linearLayout.getChildCount() == 0)
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
//
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
        linearLayout.addView(view);
    }
}
