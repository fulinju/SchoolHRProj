package sl.hr_client.main.activity;

import android.animation.Animator;
import android.annotation.TargetApi;
import android.content.res.Configuration;
import android.graphics.Color;
import android.graphics.drawable.BitmapDrawable;
import android.os.Build;
import android.os.Bundle;
import android.support.v4.app.Fragment;
import android.support.v4.widget.DrawerLayout;
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
import sl.hr_client.base.activity.BaseActivity;
import sl.hr_client.main.fragment.ContentFragment;
import sl.hr_client.main.news.fragment.NewsFragment;

/**
 * Created by xuzhijix on 2017/2/25.
 * 主页面
 */
public class MainActivity extends BaseActivity implements ViewAnimator.ViewAnimatorListener {
    private DrawerLayout drawerLayout;
    private ActionBarDrawerToggle drawerToggle;
    private List<SlideMenuItem> list = new ArrayList<>();
    private ContentFragment contentFragment;
    private ViewAnimator viewAnimator;
    private int res = R.mipmap.content_music;
    private LinearLayout linearLayout;

    private NewsFragment newsFragment;


    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

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

    private void initFragment(){
        newsFragment = new NewsFragment();
        getSupportFragmentManager().beginTransaction()
                .add(R.id.content_frame, newsFragment,NewsFragment.NEWS).commit();

        getSupportFragmentManager().beginTransaction()
                .show(newsFragment).commit();

        viewAnimator = new ViewAnimator<>(this, list, newsFragment, drawerLayout, this);


//        contentFragment = ContentFragment.newInstance(R.mipmap.content_music);
//        getSupportFragmentManager().beginTransaction()
//                .replace(R.id.content_frame, contentFragment)
//                .commit();
    }

    private void createMenuList() {
        SlideMenuItem menuItem0 = new SlideMenuItem(ContentFragment.CLOSE, R.mipmap.icn_close);
        list.add(menuItem0);
        SlideMenuItem newsItem = new SlideMenuItem(NewsFragment.NEWS, R.mipmap.icn_1);
        list.add(newsItem);
//        NewsFragment newsFragment = new NewsFragment();
//        getSupportFragmentManager().beginTransaction().add(R.id.content_frame, newsFragment,NewsFragment.NEWS).commit();
//        getSupportFragmentManager().beginTransaction().show(newsFragment).commit();
        SlideMenuItem menuItem = new SlideMenuItem(ContentFragment.BUILDING, R.mipmap.icn_1);
        list.add(menuItem);
//        SlideMenuItem menuItem2 = new SlideMenuItem(ContentFragment.BOOK, R.mipmap.icn_2);
//        list.add(menuItem2);
//        SlideMenuItem menuItem3 = new SlideMenuItem(ContentFragment.PAINT, R.mipmap.icn_3);
//        list.add(menuItem3);
//        SlideMenuItem menuItem4 = new SlideMenuItem(ContentFragment.CASE, R.mipmap.icn_4);
//        list.add(menuItem4);
//        SlideMenuItem menuItem5 = new SlideMenuItem(ContentFragment.SHOP, R.mipmap.icn_5);
//        list.add(menuItem5);
//        SlideMenuItem menuItem6 = new SlideMenuItem(ContentFragment.PARTY, R.mipmap.icn_6);
//        list.add(menuItem6);
//        SlideMenuItem menuItem7 = new SlideMenuItem(ContentFragment.MOVIE, R.mipmap.icn_7);
//        list.add(menuItem7);
    }


    private void setActionBar() {
        Toolbar toolbar = (Toolbar) findViewById(R.id.toolbar);
        setSupportActionBar(toolbar);
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




    @Override
    public ScreenShotAble onSwitch(ResourceAble slideMenuItem, ScreenShotAble screenShotAble, int position) {
        switch (slideMenuItem.getName()) {
            case ContentFragment.CLOSE:
                return screenShotAble;
            case NewsFragment.NEWS:

                return replaceFragment(newsFragment,screenShotAble,position);
            case ContentFragment.BUILDING:
                ContentFragment bu = new ContentFragment();
                return replaceFragment(bu,screenShotAble,position);
            default:
                return screenShotAble;
        }
    }

    //    @TargetApi(Build.VERSION_CODES.JELLY_BEAN)
    private ScreenShotAble replaceFragment(ContentFragment targetFragment, ScreenShotAble screenShotAble, int position) {
//        this.res = this.res == R.mipmap.content_music ? R.mipmap.content_films : R.mipmap.content_music;
        startAnim(screenShotAble,position);
//        ContentFragment contentFragment = ContentFragment.newInstance(this.res);
//        getSupportFragmentManager().beginTransaction().replace(R.id.content_frame, targetFragment).commit();
//        getSupportFragmentManager().findFragmentByTag(NewsFragment.NEWS);
        getSupportFragmentManager().beginTransaction().show(targetFragment).commit();
        return targetFragment;
    }


    private void startAnim(ScreenShotAble screenShotAble, int position) {
        View view = findViewById(R.id.content_frame);
        int finalRadius = Math.max(view.getWidth(), view.getHeight());
        Animator animator = null;
        animator = ViewAnimationUtils.createCircularReveal(view, 0, position, 0, finalRadius);

        animator.setInterpolator(new AccelerateInterpolator());
        animator.setDuration(ViewAnimator.CIRCULAR_REVEAL_ANIMATION_DURATION);

//        if (Build.VERSION.SDK_INT >= Build.VERSION_CODES.JELLY_BEAN) {
//            findViewById(R.id.content_overlay).setBackground(new BitmapDrawable(getResources(), screenShotAble.getBitmap()));
//        }else{
//            findViewById(R.id.content_overlay).setBackgroundDrawable(new BitmapDrawable(getResources(), screenShotAble.getBitmap()));
//        }

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
