package se.jockeg.OrienteeringTvResults.LayoutService.entities;

import com.fasterxml.jackson.annotation.JsonIgnore;

import javax.persistence.*;
import java.io.Serializable;
import java.util.ArrayList;
import java.util.Comparator;
import java.util.List;
import java.util.stream.Collectors;

@Entity
@Table(name = "layout")
public class Layout implements Serializable {

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    @Column(name = "id", unique = true, nullable = false)
    @JsonIgnore
    private int id;

    @Column(name = "name", unique = true, nullable = false)
    private String name;

    public String getName(){
        return name;
    }

    @OneToMany(
        mappedBy = "layout",
        cascade = CascadeType.ALL,
        orphanRemoval = true
    )
    private List<LayoutRow> rows = new ArrayList<>();

    public List<LayoutRow> getRows(){
        return rows.stream().sorted(Comparator.comparing(LayoutRow::getOrdinal)).collect(Collectors.toList());
    }

    public Layout() {
    }

    public Layout(String name) {
        this();
        this.name = name;
    }
}
