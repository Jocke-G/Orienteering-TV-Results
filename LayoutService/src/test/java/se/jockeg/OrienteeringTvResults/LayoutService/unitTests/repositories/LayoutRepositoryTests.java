package se.jockeg.OrienteeringTvResults.LayoutService.unitTests.repositories;

import com.google.common.collect.Iterables;
import org.junit.Test;
import org.junit.runner.RunWith;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.autoconfigure.orm.jpa.DataJpaTest;
import org.springframework.boot.test.autoconfigure.orm.jpa.TestEntityManager;
import org.springframework.test.context.junit4.SpringRunner;
import se.jockeg.OrienteeringTvResults.LayoutService.entities.Layout;
import se.jockeg.OrienteeringTvResults.LayoutService.repositories.LayoutRepository;

import java.util.stream.StreamSupport;

import static org.junit.Assert.*;

@RunWith(SpringRunner.class)
@DataJpaTest
public class LayoutRepositoryTests {

    @Autowired
    private TestEntityManager entityManager;

    @Autowired
    private LayoutRepository layoutRepository;

    @Test
    public void testFindAll_layoutsExists_returnLayoutList() {
        var layout1 = new Layout("TV1");
        entityManager.persist(layout1);
        var layout2 = new Layout("TV2");
        entityManager.persist(layout2);
        entityManager.flush();

        var actual = layoutRepository.findAll();

        var actualCount = StreamSupport.stream(actual.spliterator(), false).count();
        assertEquals(actualCount, 2);
        var firstActual = Iterables.get(actual, 0);
        assertEquals(firstActual.getName(), "TV1");
        var secondActual = Iterables.get(actual, 1);
        assertEquals(secondActual.getName(), "TV2");
    }

    @Test
    public void testFindAll_noLayoutsExists_returnEmptyList() {
        var actual = layoutRepository.findAll();

        var actualCount = StreamSupport.stream(actual.spliterator(), false).count();
        assertEquals(actualCount, 0);
    }

    @Test
    public void testFindByName_layoutExists_returnLayout() {
        var layout = new Layout("TV4");
        entityManager.persist(layout);
        entityManager.flush();

        Layout actual = layoutRepository.findByName("TV4");

        assertEquals(actual.getName(), "TV4");
    }

    @Test
    public void testGetByName_layoutDontExists_returnNull() {
        var actual = layoutRepository.findByName("TV4");

        assertNull(actual);
    }
}
