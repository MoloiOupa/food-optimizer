<?php

$inventory = [
    'Cucumber' => 2,
    'Olives'   => 2,
    'Lettuce'  => 3,
    'Meat'     => 6,
    'Tomato'   => 6,
    'Cheese'   => 8,
    'Dough'    => 10,
];

$recipes = [
    ['name' => 'Sandwich', 'feeds' => 1, 'ingredients' => ['Dough' => 1, 'Lettuce' => 1, 'Cucumber' => 1, 'Cheese' => 1]],
    ['name' => 'Burger',   'feeds' => 1, 'ingredients' => ['Meat' => 1, 'Dough' => 2, 'Tomato' => 1, 'Cheese' => 1]],
    ['name' => 'Pie',      'feeds' => 1, 'ingredients' => ['Meat' => 2, 'Dough' => 1]],
    ['name' => 'Pasta',    'feeds' => 2, 'ingredients' => ['Dough' => 2, 'Meat' => 1, 'Cheese' => 2]],
    ['name' => 'Salad',    'feeds' => 3, 'ingredients' => ['Lettuce' => 2, 'Tomato' => 1, 'Olives' => 1]],
    ['name' => 'Pizza',    'feeds' => 4, 'ingredients' => ['Dough' => 3, 'Tomato' => 2, 'Cheese' => 3, 'Olives' => 1]],
];

$maxPeopleFed = 0;
$bestCombination = [];

function canMake(array $recipe, array $inventory): bool {
    foreach ($recipe['ingredients'] as $ingredient => $count) {
        if (!isset($inventory[$ingredient]) || $inventory[$ingredient] < $count) {
            return false;
        }
    }
    return true;
}

function findOptimalCombination(int $index, array $inventory, array $recipes, array $currentCombo, int $currentFed) {
    global $maxPeopleFed, $bestCombination;

    if ($currentFed > $maxPeopleFed) {
        $maxPeopleFed = $currentFed;
        $bestCombination = $currentCombo;
    }

    if ($index >= count($recipes)) {
        return;
    }

    $recipe = $recipes[$index];
    $count = 0;

    while (canMake($recipe, $inventory)) {
        foreach ($recipe['ingredients'] as $ing => $qty) {
            $inventory[$ing] -= $qty;
        }
        $count++;
        $currentCombo[$recipe['name']] = $count;

        findOptimalCombination($index + 1, $inventory, $recipes, $currentCombo, $currentFed + ($count * $recipe['feeds']));
    }

    while ($count > 0) {
        foreach ($recipe['ingredients'] as $ing => $qty) {
            $inventory[$ing] += $qty;
        }
        $count--;
    }
    $currentCombo[$recipe['name']] = 0;

    findOptimalCombination($index + 1, $inventory, $recipes, $currentCombo, $currentFed);
}

findOptimalCombination(0, $inventory, $recipes, [], 0);

echo "Maximum people fed: " . $maxPeopleFed . "\n";
echo "Optimal combination:\n";
foreach ($bestCombination as $recipeName => $qty) {
    if ($qty > 0) {
        echo "- {$qty} x {$recipeName}\n";
    }
}