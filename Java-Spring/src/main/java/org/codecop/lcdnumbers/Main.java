package org.codecop.lcdnumbers;

import java.util.List;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.ApplicationArguments;
import org.springframework.boot.ApplicationRunner;
import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;

@SpringBootApplication
public class Main implements ApplicationRunner {

    @Autowired
    private ScalingArgument scalingArgument;
    @Autowired
    private LcdDisplay lcdDisplay;

    @Override
    public void run(ApplicationArguments args) {

        List<String> nonOptionArgs = args.getNonOptionArgs();
        if (nonOptionArgs.size() == 0) {
            System.out.println("Run this class to see LCD Numbers working:");
            System.out.println("\nRunning the generated jar:");
            System.out.println("java -jar target/lcd-numbers-di-framework-kata-1.0.0-SNAPSHOT.jar 12345 2");
            System.out.println("\nRunning via Maven:");
            System.out.println("mvnw spring-boot:run -Dspring-boot.run.arguments=12345,2");
            return;
        }

        int number = Integer.parseInt(nonOptionArgs.get(0));

        System.out.print(lcdDisplay.toLcd(number, scalingArgument.getScaling()));
    }

    public static void main(String[] args) {
        SpringApplication.run(Main.class, args);
    }

}
