package se.jockeg.OrienteeringTvResults.LayoutService.repositories;

import org.springframework.data.repository.CrudRepository;
import se.jockeg.OrienteeringTvResults.LayoutService.entities.Layout;

public interface LayoutRepository extends CrudRepository<Layout, Integer> {
    Layout findByName(String name);
}
