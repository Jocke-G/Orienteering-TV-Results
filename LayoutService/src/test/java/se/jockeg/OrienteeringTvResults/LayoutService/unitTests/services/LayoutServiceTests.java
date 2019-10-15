package se.jockeg.OrienteeringTvResults.LayoutService.unitTests.services;

import org.junit.*;
import org.junit.runner.RunWith;
import org.mockito.Mockito;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.context.TestConfiguration;
import org.springframework.boot.test.mock.mockito.MockBean;
import org.springframework.context.annotation.Bean;
import org.springframework.test.context.junit4.SpringRunner;
import se.jockeg.OrienteeringTvResults.LayoutService.entities.Layout;
import se.jockeg.OrienteeringTvResults.LayoutService.exceptions.LayoutNotFoundException;
import se.jockeg.OrienteeringTvResults.LayoutService.repositories.LayoutRepository;
import se.jockeg.OrienteeringTvResults.LayoutService.services.LayoutService;
import se.jockeg.OrienteeringTvResults.LayoutService.services.LayoutServiceImpl;

import java.util.ArrayList;
import java.util.stream.StreamSupport;

import static junit.framework.TestCase.*;

@RunWith(SpringRunner.class)
public class LayoutServiceTests {

    @TestConfiguration
    static class EmployeeServiceImplTestContextConfiguration {

        @Bean
        public LayoutService layoutService() {
            return new LayoutServiceImpl();
        }
    }

    @Autowired
    private LayoutService layoutService;

    @MockBean
    private LayoutRepository layoutRepository;

    @Test
    public void testGetLayouts_layoutsExists_returnLayoutList() {
        var layout1 = new Layout("TV1");
        var layout2 = new Layout("TV2");
        var layoutList = new ArrayList<Layout>();
        layoutList.add(layout1);
        layoutList.add(layout2);

        Mockito.when(layoutRepository.findAll())
                .thenReturn(layoutList);

        var actual = layoutService.getLayouts();

        var actualCount = StreamSupport.stream(actual.spliterator(), false).count();
        assertEquals(actualCount, 2);
    }

    @Test
    public void testGetLayouts_noLayoutsExists_returnEmptyList() {
        var actual = layoutService.getLayouts();

        var actualCount = StreamSupport.stream(actual.spliterator(), false).count();
        assertEquals(actualCount, 0);
    }

    @Test
    public void testGetLayout_layoutExists_returnLayout() {
        var layout = new Layout("TV1");

        Mockito.when(layoutRepository.findByName(layout.getName()))
                .thenReturn(layout);

        var name = "TV1";
        var actual = layoutService.getLayout(name);

        assertEquals(actual.getName(), name);
    }

    @Test(expected = LayoutNotFoundException.class)
    public void testGetLayout_layoutDontExists_returnNull() {
        var name = "TV1";

        var actual = layoutService.getLayout(name);

        assertNull(actual);
    }
}
